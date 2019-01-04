using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICinemaService : IBaseService<Cinema, Guid> 
    {
        Task<List<Cinema>> GetEagerAllAsync();
        Task<List<Cinema>> GetPagedAsync(int page, int size, string orderBy, bool order);
        Task<Cinema> GetEagerByIdAsync(Guid id);
        Task<List<Cinema>> GetEagerByIdsAsync(List<Guid> ids);
    }
}
