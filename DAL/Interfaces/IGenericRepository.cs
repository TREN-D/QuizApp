using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<TEntity, TKey>
        : IBaseRepository where TEntity : class, IGenericBaseEntity<TKey>
    {
        IQueryable<TEntity> Get(bool eager = false, bool track = false);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken, bool eager = false,
                                bool track = true, bool shouldThrow = true);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> DeleteAsync(TKey id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
