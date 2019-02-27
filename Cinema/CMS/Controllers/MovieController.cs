using AutoMapper;
using CMS.Models;
using CMS.Models.CinemaMovie;
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
                var movies = await movieService.GetPagedAsync(pagedQuery, page - 1, size);
                var count = await movieService.GetPagedCountAsync(pagedQuery);

                var viewItems = mapper.Map<List<MovieIndexViewModel>>(movies);
                var dto = new PagedViewModel<MovieIndexViewModel>(
                    viewItems, page, size, orderBy, isAsc, filter, isExact, count);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(id);
                if (movie is null)
                {
                    return NotFound();
                }

                var dto = mapper.Map<MovieDetailsViewModel>(movie);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Create()
        {
            var dto = new MovieCreateViewModel();

            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateViewModel dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var movie = mapper.Map<Movie>(dto);
                await movieService.CreateAsync(movie);

                return RedirectToAction(nameof(Details), new { id = movie.Id });
            }
            catch
            {
                return View(dto);
            }
        }

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
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieEditViewModel dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var movie = mapper.Map<Movie>(dto);
                await movieService.UpdateAsync(movie);

                return RedirectToAction(nameof(Details), new { id = movie.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(id);
                var dto = mapper.Map<MovieDeleteViewModel>(movie);

                return View(dto);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MovieDeleteViewModel dto)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(dto.Id);
                await movieService.DeleteAsync(movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> AssignMovie(Guid id)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(id);

                if (movie is null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var cinemas = await cinemaService.GetAllAsync();
                var cinemaMovie = await cinemaMovieService.GetAllByMovieIdAsync(id);

                List<CinemaMovieModel> cinemaMovieDto = cinemas
                    .Select(x => new CinemaMovieModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList();

                var dto = new CinemaMovieViewModel
                {
                    MovieId = movie.Id,
                    MovieName = movie.Name
                };

                cinemaMovieDto.ForEach(cinema =>
                {
                    var movieInCinema = cinemaMovie.Find(x => x.CinemaId == cinema.Id);
                    dto.CinemaDictionary.Add(cinema, movieInCinema != null ? true : false);
                });

                return View(dto);

            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignMovie(CinemaMovieViewModel model)
        {
            try
            {
                var existingCinemaMovie = await cinemaMovieService.GetAllByMovieIdAsync(model.MovieId);
                var cinemaIds = new List<Guid>();

                cinemaIds = model.CinemaDictionary
                    .Where(cinema => cinema.Value)
                    .Select(cinema => cinema.Key.Id)
                    .ToList();

                #region Delete
                if (!cinemaIds.Any())
                {
                    foreach (var cinema in existingCinemaMovie)
                    {
                        await cinemaMovieService.Delete(cinema);
                    }

                    return RedirectToAction("Index", "Movie");
                }
                #endregion
                #region Update
                if (existingCinemaMovie.Any())
                {
                    var addedCinemasToExistentMovie = cinemaIds
                        .Except(existingCinemaMovie
                        .Select(cm => cm.CinemaId))
                        .ToList();

                    if (addedCinemasToExistentMovie.Any())
                    {
                        var addCinemaMovie = addedCinemasToExistentMovie
                            .Select(cinemaId => new CinemaMovie
                            {
                                MovieId = model.MovieId,
                                CinemaId = cinemaId
                            }).ToList();

                        await cinemaMovieService.CreateAsync(addCinemaMovie);
                    }
                    else
                    {
                        var removedCinemasToExistentMovide = existingCinemaMovie
                            .Select(x => x.CinemaId)
                            .Except(cinemaIds)
                            .ToList();

                        var movieCinemaToBeDeleted = removedCinemasToExistentMovide
                            .Select(cinemaId => new CinemaMovie
                            {
                                MovieId = model.MovieId,
                                CinemaId = cinemaId
                            }).ToList();

                        await cinemaMovieService.Delete(movieCinemaToBeDeleted);

                    }

                    return RedirectToAction("Index", "Movie");
                }
                #endregion
                #region Create
                else
                {
                    var newCinemas = cinemaIds.Select(cinemaId => new CinemaMovie
                    {
                        MovieId = model.MovieId,
                        CinemaId = cinemaId
                    }).ToList();

                    await cinemaMovieService.CreateAsync(newCinemas);
                    return RedirectToAction("Index", "Movie");
                }
                #endregion
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
        }
    }
}
