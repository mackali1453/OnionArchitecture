using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Github.NetCoreWebApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while creating entity.", ex);
            }
        }
        public async Task<List<T>> GetAll()
        {
            List<T> allData;
            try
            {
                allData = await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while creating entity.", ex);
            }

            return allData;
        }
        public async Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await _dbContext.Set<T>().AsNoTracking().Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching entities by filter.", ex);
            }
        }

        public async Task<List<T>?> GetByFilterEager(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbContext.Set<T>();

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return await query.Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching entity by filter with eager loading.", ex);
            }
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching entity by ID.", ex);
            }
        }
        public void Remove(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while removing entity.", ex);
            }
        }

        public async Task RemoveEager(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                var entityToDelete = await query.SingleOrDefaultAsync(filter);

                if (entityToDelete != null)
                {
                    _dbContext.Set<T>().Remove(entityToDelete);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while removing entity with eager loading.", ex);
            }
        }

        public async Task UpdateAsync(T updatedEntity, T oldEntity)
        {
            try
            {
                _dbContext.Entry(oldEntity).CurrentValues.SetValues(updatedEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating entity.", ex);
            }
        }
        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    _dbContext.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating product entities.", ex);
            }
        }
    }
}
