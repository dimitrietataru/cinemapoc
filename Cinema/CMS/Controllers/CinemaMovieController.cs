using AutoMapper;
using CMS.Models.CinemaMovie;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class CinemaMovieController : Controller
    {
        private readonly ICinemaMovieService cinemaMovieService;
        private readonly ICinemaService cinemaService;
        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public CinemaMovieController(
            ICinemaMovieService cinemaMovieService,
            ICinemaService cinemaService,
            IMovieService movieService,
            IMapper mapper)
        {
            this.cinemaMovieService = cinemaMovieService;
            this.cinemaService = cinemaService;
            this.movieService = movieService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Assign(Guid movieId)
        {
            try
            {
                var movie = await movieService.GetByIdAsync(movieId);
                var cinemas = await cinemaService.GetAllAsync();
                var cinemaMovies = await cinemaMovieService.GetByMovieIdAsync(movie.Id);

                var movieDto = mapper.Map<MovieAssignViewModel>(movie);
                var cinemaDtos = mapper.Map<List<CinemaAssignViewModel>>(cinemas);
                cinemaDtos
                    .Join(cinemaMovies, c => c.Id, cm => cm.CinemaId, (cinema, _) => cinema)
                    .ToList()
                    .ForEach(cinema => cinema.IsAssigned = true);

                return View(new CinemaMovieViewModel(movieDto, cinemaDtos));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(CinemaMovieViewModel dto)
        {
            try
            {
                var cinemaAssignments = mapper.Map<List<KeyValuePair<Guid, bool>>>(dto.Cinemas);
                await cinemaMovieService.SyncAsync(dto.Movie.Id, cinemaAssignments);

                return RedirectToAction("Details", "Movie", new { id = dto.Movie.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
