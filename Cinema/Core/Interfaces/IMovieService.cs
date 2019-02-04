using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMovieService : IBaseService<Movie, Guid>
    {
        IQueryable<Movie> GetPagedQuery(string orderBy, bool order, string filter, bool isExact);
        Task<List<Movie>> GetPagedAsync(IQueryable<Movie> query, int page, int size);
        Task<int> GetPagedCountAsync(IQueryable<Movie> pagedQuery);
    }
}
