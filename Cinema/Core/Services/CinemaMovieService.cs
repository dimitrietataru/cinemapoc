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

        public async Task AsignCinemaToMovies(Guid movieId, List<KeyValuePair<Guid, bool>> cinemas)
        {
            var currentAssignments = GetByMovieIdAsync(movieId);
        }
    }
}
