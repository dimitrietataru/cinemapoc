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
    public class CinemaService : BaseService<Cinema, Guid>, ICinemaService
    {
        public CinemaService(CinemaContext context) : base(context)
        {
        }
        
        public async Task<List<Cinema>> GetEagerAllAsync()
        {
            return await context
               .Cinemas
               .AsNoTracking()
               .Include(cinema => cinema.Auditoriums)
               .Include(cinema => cinema.CinemaMovies)
               .ThenInclude(cinemaMovie => cinemaMovie.Movie)
               .ToListAsync();
        }

        public async Task<Cinema> GetEagerByIdAsync(Guid id)
        {
            return await context
               .Cinemas
               .AsNoTracking()
               .Include(cinema => cinema.Auditoriums)
               .Include(cinema => cinema.CinemaMovies)
               .ThenInclude(cinemaMovie => cinemaMovie.Movie)
               .FirstOrDefaultAsync(cinema => cinema.Id.Equals(id));
        }

        public async Task<List<Cinema>> GetEagerByIdsAsync(List<Guid> ids)
        {
            return await context
                .Cinemas
                .AsNoTracking()
                .Include(cinema => cinema.Auditoriums)
                .Include(cinema => cinema.CinemaMovies)
                .ThenInclude(cinemaMovie => cinemaMovie.Movie)
                .Join(
                    ids,
                    cinema => cinema.Id,
                    id => id,
                    (cinema, id) => cinema)
                .ToListAsync();
        }
    }
}
