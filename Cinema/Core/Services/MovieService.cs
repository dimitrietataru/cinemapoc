using Core.Context;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MovieService : BaseService<Movie, Guid>, IMovieService
    {
        public MovieService(CinemaContext context) : base(context)
        {
        }

        public async Task<List<Movie>> GetPagedAsync(int page, string orderBy, bool orderDesc, string matchString = null)
        {
            var query = context
                    .Movies
                    .AsNoTracking();

            if (!string.IsNullOrEmpty(matchString))
            {
                query = query.Where(movie => matchString.Equals(movie.Name));
            }

            return await query
                    .Skip((page - 1) * 10)
                    .Take(10)
                    .OrderBy(cinema => orderDesc ? cinema.GetType().GetProperty(orderBy).GetValue(cinema, null) : null)
                    .OrderByDescending(cinema => !orderDesc ? cinema.GetType().GetProperty(orderBy).GetValue(cinema, null) : null)
               .ToListAsync();
        }
    }
}
