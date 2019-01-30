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

        public IQueryable<Cinema> GetPagedQuery(string orderBy, bool order)
        {
            var query = GetBaseQuery();

            ////string filter = "";
            ////query = query.Where(cinema =>
            ////    cinema.Name.Contains(filter)
            ////    || cinema.Address.Contains(filter)
            ////    || cinema.Contact.Contains(filter)
            ////    || cinema.Location.Contains(filter));

            switch (orderBy)
            {
                case "Name" when order is true:
                    query = query.OrderBy(cinema => cinema.Name);
                    break;
                case "Name" when order is false:
                    query = query.OrderByDescending(cinema => cinema.Name);
                    break;
                case "Location" when order is true:
                    query = query.OrderBy(cinema => cinema.Location);
                    break;
                case "Location" when order is false:
                    query = query.OrderByDescending(cinema => cinema.Location);
                    break;
                default:
                    break;
            }

            return query;
        }

        public async Task<List<Cinema>> GetPagedAsync(IQueryable<Cinema> query, int page, int size)
        {
            return await query
                .Skip(page * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetPagedCountAsync(IQueryable<Cinema> pagedQuery)
        {
            return await pagedQuery.CountAsync();
        }
    }
}
