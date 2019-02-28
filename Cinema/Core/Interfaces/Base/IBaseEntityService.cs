using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Base
{
    public interface IBaseEntityService<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetQueriable(bool withTrack = false);
        Task<int> GetCountAsync();
        Task<long> GetLongCountAsync();
        Task CreateAsync(TEntity entity);
        Task CreateAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(List<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(List<TEntity> entities);
        Task SaveAsync();
    }
}
