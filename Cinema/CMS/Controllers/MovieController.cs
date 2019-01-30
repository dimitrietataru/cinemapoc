using AutoMapper;
using CMS.Models.Movie;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
                int allMoviesCount = 0;
                var movies = await movieService.GetPagedAsync(page, orderBy, orderDesc, matchString);

                allMoviesCount = !string.IsNullOrEmpty(matchString)
                    ? allMoviesCount = movies.Count
                    : allMoviesCount = await movieService.GetCountAsync();

                var dto = new MovieIndexViewModel(movies, page, 10, allMoviesCount)
                {
                    OrderBy = orderBy,
                    OrderDesc = orderDesc
                };

                return View(dto);
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Home");
            }
        }
    }
}