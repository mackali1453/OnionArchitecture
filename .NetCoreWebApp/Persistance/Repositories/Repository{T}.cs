using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Github.NetCoreWebApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private DbContext _webApiContext;

        public Repository(DbContext webApiContext)
        {
            _webApiContext = webApiContext;
        }
        public async Task CreateAsync(T entity)
        {
            try
            {
                await _webApiContext.Set<T>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            List<T> result;

            try
            {
                result = await _webApiContext.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return result;
        }

        public async Task<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            T result;

            try
            {
                result = await _webApiContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return result;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            T result;

            try
            {
                result = await _webApiContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return result;
        }

        public Task RemoveAsyn(T entity)
        {
            Task result;

            try
            {
                result = Task.FromResult(() => { _webApiContext.Set<T>().Remove(entity); });
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return result;
        }
        public Task UpdateAsync(T updatedEntity, T oldEntity)
        {
            Task<Action> action;

            try
            {
                action = Task.FromResult(() =>
                {
                    _webApiContext.Entry(oldEntity).CurrentValues.SetValues(updatedEntity);
                });
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return action;
        }
    }
}
