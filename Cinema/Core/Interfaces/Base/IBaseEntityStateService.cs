using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Base
{
    public interface IBaseEntityStateService<TEntity>
        where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task CreateAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task SaveAsync();
    }
}
