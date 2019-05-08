using DAL.Entities;
using DAL.Interfaces;
using DAL.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
namespace DAL.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IGenericBaseEntity<TKey>
    {
        protected readonly IApplicationDbContext _context;

        public GenericRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var addedEntity = _context.DbContext.Set<TEntity>().Add(entity);
            await SaveChangesAsync(cancellationToken);
            return addedEntity;
        }

        public virtual async Task<TEntity> DeleteAsync(TKey id, CancellationToken cancellationToken)
        {
            var entityToDelete = await GetAsync(id, cancellationToken, shouldThrow: false);

            if (entityToDelete != null)
            {
                _context.DbContext.Set<TEntity>().Remove(entityToDelete);
                await SaveChangesAsync(cancellationToken);
            }
            return entityToDelete;
        }

        public virtual async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            entities = _context.DbContext.Set<TEntity>().RemoveRange(entities);
            await SaveChangesAsync(cancellationToken);
            return entities;
        }

        public virtual IQueryable<TEntity> Get(bool eager = false, bool track = false)
        {
            var result = _context.DbContext.Set<TEntity>().AsQueryable();

            if (eager)
            {
                var properties = typeof(TEntity).GetProperties();

                foreach (var property in properties)
                {
                    var isVirtual = property.GetMethod.IsVirtual;
                    var isFinal = property.GetMethod.IsFinal;
                    if (isVirtual && !isFinal)
                    {
                        result = result.Include(property.Name);
                    }
                }
            }

            if (!track)
            {
                result = result.AsNoTracking();
            }
            return result;
        }

        public virtual async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken, bool eager = false,
                                                    bool track = true, bool shouldThrow = true)
        {
            var query = Get(eager, track);
            TEntity result = null;

            try
            {
                // TKey is upcasted to object, because compiler can't decide what should be used, class or struct comparer. In our case TKey can be any of it.
                result = await query
                                    .SingleAsync(e => (object)e.Id == (object)id, cancellationToken);
            }
            catch
            {
                if (shouldThrow)
                {
                    throw new NotFoundException($"Can't find an instance with id {id} of {typeof(TEntity).Name} entity.");
                }
            }
            return result;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.DbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
