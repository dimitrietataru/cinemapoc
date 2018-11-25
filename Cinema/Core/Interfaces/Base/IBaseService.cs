using Core.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseService<TEntity, TId>
        where TEntity : class, IEntity<TId>, IDeletable
        where TId : struct
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
        Task<List<TEntity>> GetByIdsAsync(List<TId> ids);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SaveAsync();
    }
}
