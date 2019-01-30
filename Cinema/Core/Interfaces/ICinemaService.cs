using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICinemaService : IBaseService<Cinema, Guid> 
    {
        Task<List<Cinema>> GetEagerAllAsync();
        Task<Cinema> GetEagerByIdAsync(Guid id);
        Task<List<Cinema>> GetEagerByIdsAsync(List<Guid> ids);

        IQueryable<Cinema> GetPagedQuery(string orderBy, bool order);
        Task<List<Cinema>> GetPagedAsync(IQueryable<Cinema> query, int page, int size);
        Task<int> GetPagedCountAsync(IQueryable<Cinema> pagedQuery);
    }
}
