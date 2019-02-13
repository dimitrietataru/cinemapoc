using Core.Context;
using Core.Interfaces;
using Core.Models.Base;
using Core.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public abstract class BaseService<TEntity, TId> : BaseEntityStateService<TEntity>, IBaseService<TEntity, TId>
        where TEntity : class, IEntity<TId>, IDeletable
        where TId : struct
    {
        protected readonly CinemaContext context;

        public BaseService(CinemaContext context) : base(context) => this.context = context;
        
        public async virtual Task<List<TEntity>> GetAllAsync()
        {
            return await context
                .Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async virtual Task<TEntity> GetByIdAsync(TId id)
        {
            return await context
                .Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public async virtual Task<List<TEntity>> GetByIdsAsync(List<TId> ids)
        {
            return await context
                .Set<TEntity>()
                .AsNoTracking()
                .Join(
                    ids,
                    entity => entity.Id,
                    id => id,
                    (entity, id) => entity)
                .ToListAsync();
        }

        public async virtual Task DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public IQueryable<TEntity> GetBaseQuery(bool isTracked = false)
        {
            return isTracked
                ? context.Set<TEntity>()
                : context.Set<TEntity>().AsNoTracking();
        }

        public async Task<int> GetCountAsync()
        {
            return await context.Set<TEntity>().CountAsync();
        }
    }
}
