using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using GameFinder.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class

    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }        
    
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public async Task<TEntity> AddSingleAsync(TEntity entity)
        {
            var result =  (await _dbSet.AddAsync(entity)).Entity;
            await SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSingleAsync(TEntity entity)
        {
            _dbSet.RemoveRange(entity);
            await SaveChangesAsync();
            return await Task.FromResult(true);

        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true)
        {
            if (predicate == null)
                predicate = (TEntity) => true;

            if (withTracking)
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true)
        {
            if (predicate == null)
                predicate = (TEntity) => true;
            if (withTracking)
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }

            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
