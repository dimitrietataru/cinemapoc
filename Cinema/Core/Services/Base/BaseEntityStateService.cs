using Core.Context;
using Core.Interfaces.Base;
using System.Threading.Tasks;

namespace Core.Services.Base
{
    public abstract class BaseEntityStateService<TEntity> : IBaseEntityStateService<TEntity>
        where TEntity : class
    {
        private readonly CinemaContext context;

        public BaseEntityStateService(CinemaContext context) => this.context = context;

        public async virtual Task CreateAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async virtual Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
