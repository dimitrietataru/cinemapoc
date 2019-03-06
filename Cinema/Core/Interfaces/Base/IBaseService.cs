using Core.Interfaces.Base;
using Core.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseService<TEntity, TId> : IBaseEntityService<TEntity>
        where TEntity : class, IEntity<TId>, IDeletable, IStableEntity
        where TId : struct
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task<List<TEntity>> GetByIdsAsync(List<TId> ids);
        new Task UpdateAsync(TEntity entity);
        new Task UpdateAsync(List<TEntity> entities);
        new Task DeleteAsync(TEntity entity);
        new Task DeleteAsync(List<TEntity> entities);
    }
}
