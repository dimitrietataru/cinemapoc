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

        public IQueryable<Movie> GetPagedQuery(string orderBy, bool order, string filter, bool isExact)
        {
            var query = GetBaseQuery();

            if (!string.IsNullOrWhiteSpace(filter) && isExact)
            {
                query = query.Where(movie => movie.Name.Equals(filter) || movie.Studio.Equals(filter));
            }
            else if (!string.IsNullOrWhiteSpace(filter) && !isExact)
            {
                filter = filter.ToLower();
                query = query
                    .Where(movie =>
                        movie.Name.ToLower().Contains(filter)
                        || movie.Studio.ToLower().Contains(filter));
            }

            switch (orderBy)
            {
                case "Name" when order is true:
                    query = query.OrderBy(movie => movie.Name);
                    break;
                case "Name" when order is false:
                    query = query.OrderByDescending(movie => movie.Name);
                    break;
                case "Duration" when order is true:
                    query = query.OrderBy(movie => movie.Duration);
                    break;
                case "Duration" when order is false:
                    query = query.OrderByDescending(movie => movie.Duration);
                    break;
                case "Studio" when order is true:
                    query = query.OrderBy(movie => movie.Studio);
                    break;
                case "Studio" when order is false:
                    query = query.OrderByDescending(movie => movie.Studio);
                    break;
                case "StartDate" when order is true:
                    query = query.OrderBy(movie => movie.StartDate);
                    break;
                case "StartDate" when order is false:
                    query = query.OrderByDescending(movie => movie.StartDate);
                    break;
                case "EndDate" when order is true:
                    query = query.OrderBy(movie => movie.EndDate);
                    break;
                case "EndDate" when order is false:
                    query = query.OrderByDescending(movie => movie.EndDate);
                    break;
                default:
                    break;
            }

            return query;
        }

        public async Task<List<Movie>> GetPagedAsync(IQueryable<Movie> query, int page, int size)
        {
            return await query
                .Skip(page * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetPagedCountAsync(IQueryable<Movie> pagedQuery)
        {
            return await pagedQuery.CountAsync();
        }
    }
}
