using AutoMapper;
using CMS.Models.Movie;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public MovieController(
            IMovieService movieService,
            IMapper mapper)
        {
            this.movieService = movieService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string orderBy = "Name", bool orderDesc = true, string matchString = null)
        {
            try
            {
                var movies = await movieService.GetPagedAsync(page, orderBy, orderDesc, matchString);

                int allMoviesCount = !string.IsNullOrEmpty(matchString)
                    ? movies.Count
                    : await movieService.GetCountAsync();

                var dto = new MovieIndexViewModel(movies, page, 10, allMoviesCount)
                {
                    OrderBy = orderBy,
                    OrderDesc = orderDesc,
                    MatchString = matchString
                };

                return View(dto);
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task CreateMovies()
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    var movie = new Movie()
                    {
                        Name = i + "Catalin Movie",
                        Description = "Desc",
                        Duration = 0,
                        Actors = "asd",
                        Studio = "asd",
                        ProjectionType = Core.Models.Enums.ProjectionType.D2D,
                        Rating = Core.Models.Enums.MovieRating.G,
                        Genre = "asd",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now
                    };

                    await movieService.CreateAsync(movie);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
