using Core.Context;
using Core.Interfaces;
using Core.Models.Base;
using Core.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public abstract class BaseService<TEntity, TId> : BaseEntityService<TEntity>, IBaseService<TEntity, TId>
        where TEntity : class, IEntity<TId>, IDeletable, IStableEntity
        where TId : struct
    {
        private protected readonly new CinemaContext context;

        public BaseService(CinemaContext context) : base(context) => this.context = context;

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

        public async new virtual Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            await base.UpdateAsync(entity);
        }

        public async new virtual Task UpdateAsync(List<TEntity> entities)
        {
            entities.ForEach(e => e.UpdatedAt = DateTime.UtcNow);

            await base.UpdateAsync(entities);
        }

        public async new virtual Task DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;

            await UpdateAsync(entity);
        }

        public async new virtual Task DeleteAsync(List<TEntity> entities)
        {
            entities.ForEach(e => e.IsDeleted = true);

            await UpdateAsync(entities);
        }
    }
}
