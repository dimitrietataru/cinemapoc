using Core.Context;
using Core.Interfaces;
using Core.Models;
using Core.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CinemaMovieService : BaseEntityService<CinemaMovie>, ICinemaMovieService
    {
        public CinemaMovieService(CinemaContext context) : base(context)
        {
        }

        public async Task<List<CinemaMovie>> GetByMovieIdAsync(Guid movieId)
        {
            return await context
               .CinemaMovies
               .AsNoTracking()
               .Include(cinemaMovie => cinemaMovie.Movie)
               .Include(cinemaMovie => cinemaMovie.Cinema)
               .Where(movie => movie.MovieId == movieId)
               .ToListAsync();
        }

        public async Task SyncAsync(Guid movieId, List<KeyValuePair<Guid, bool>> cinemas)
        {
            var currentCinemaMovies = await GetByMovieIdAsync(movieId);
            var oldCinemaIds = currentCinemaMovies
                .Select(cinemaMovie => cinemaMovie.CinemaId)
                .ToList();

            var checkedIds = cinemas
                .Where(pair => pair.Value)
                .Select(pair => pair.Key)
                .ToList();
            var toCreateIds = checkedIds
                .Except(oldCinemaIds)
                .ToList();
            var cinemMovieToCreate = toCreateIds
                .Select(id => new CinemaMovie { MovieId = movieId, CinemaId = id })
                .ToList();

            var unckeckedIds = cinemas
                .Where(pair => !pair.Value)
                .Select(pair => pair.Key)
                .ToList();
            var toDeleteIds = unckeckedIds
                .Except(toCreateIds)
                .Where(id => oldCinemaIds.Contains(id))
                .ToList();
            var cinemMovieToDelete = toDeleteIds
                .Select(id => new CinemaMovie { MovieId = movieId, CinemaId = id })
                .ToList();

            await base.CreateAsync(cinemMovieToCreate);
            await base.DeleteAsync(cinemMovieToDelete);
        }
    }
}
