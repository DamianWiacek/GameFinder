using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, Boolean>> predicate = null, Boolean withTracking = true);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, Boolean>> predicate = null, Boolean withTracking = true);
        Task<Boolean> DeleteSingleAsync(TEntity entity);
        Task<Boolean> DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> AddSingleAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
