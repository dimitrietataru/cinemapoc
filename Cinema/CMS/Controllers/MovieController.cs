using AutoMapper;
using CMS.Models;
using CMS.Models.Movie;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly ICinemaService cinemaService;
        private readonly ICinemaMovieService cinemaMovieService;
        private readonly IMapper mapper;

        public MovieController(
            IMovieService movieService,
            ICinemaService cinemaService,
            ICinemaMovieService cinemaMovieService,
            IMapper mapper)
        {
            this.movieService = movieService;
            this.cinemaService = cinemaService;
            this.cinemaMovieService = cinemaMovieService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            int size = 10,
            string orderBy = "Name",
            bool isAsc = true,
            string filter = "",
            bool isExact = false)
        {
            try
            {
                var pagedQuery = movieService.GetPagedQuery(orderBy, isAsc, filter, isExact);
                var cinemas = await movieService.GetPagedAsync(pagedQuery, page - 1, size);
                var count = await movieService.GetPagedCountAsync(pagedQuery);

                var viewItems = mapper.Map<List<MovieIndexViewModel>>(cinemas);
                var dto = new PagedViewModel<MovieIndexViewModel>(
                    viewItems, page, size, orderBy, isAsc, filter, isExact, count);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(id);
                var dto = mapper.Map<MovieDetailsViewModel>(movie);

                return View(dto);
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
#pragma warning disable CS1998
        public async Task<IActionResult> Create()
#pragma warning restore CS1998
        {
            try
            {
                return View(new MovieCreateViewModel());
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateViewModel model)
        {
            try
            {
                var movie = mapper.Map<Movie>(model);

                await movieService.CreateAsync(movie);

                return RedirectToAction("Details", "Movie", new { id = movie.Id });
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(id);

                var dto = mapper.Map<MovieEditViewModel>(movie);

                return View(dto);
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieEditViewModel model)
        {
            try
            {
                var movie = mapper.Map<Movie>(model);

                await movieService.UpdateAsync(movie);

                return RedirectToAction("Index", "Error");
            }
            catch (Exception ex)
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(id);

                await movieService.DeleteAsync(movie);

                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignMovie(Guid id)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(id);
                var cinemas = await cinemaService.GetAllAsync();

                var dto = new CinemaMovieViewModel
                {
                    MovieId = movie.Id,
                    MovieName = movie.Name
                };

                foreach (var cinema in cinemas)
                {
                    dto.CinemaDictionary.Add(cinema, false);
                }

                return View(dto);

            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AssignMovie(CinemaMovieViewModel model)
        {
            try
            {
                var cinemaIds = new List<Guid>();

                cinemaIds = model.CinemaDictionary
                    .Where(cinema => cinema.Value)
                    .Select(cinema => cinema.Key.Id)
                    .ToList();

                foreach (var cinemaId in cinemaIds)
                {
                    var cinemaMovie = new CinemaMovie
                    {
                        MovieId = model.MovieId,
                        CinemaId = cinemaId
                    };

                    await cinemaMovieService.CreateAsync(cinemaMovie);
                }

                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                // TODO: Add proper error page and Log
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task CreateMovies()
        {
            for (int i = 0; i < 25; i++)
            {
                var movie = new Movie
                {
                    Name = i + "Movie",
                    Description = "descrip",
                    Duration = i * 5,
                    Actors = "Bruce lee",
                    Studio = "Marvel",
                    ProjectionType = Core.Models.Enums.ProjectionType.D2D,
                    Rating = Core.Models.Enums.MovieRating.G,
                    Genre = "horor",
                    TrailerUrl = "",
                    PosterUrl = "",
                    ImbdUrl = "",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };

                await movieService.CreateAsync(movie);
            }
        }
    }
}
