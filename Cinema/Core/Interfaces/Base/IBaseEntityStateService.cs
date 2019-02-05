using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Base
{
    public interface IBaseEntityStateService<TEntity>
        where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task SaveAsync();
    }
}
