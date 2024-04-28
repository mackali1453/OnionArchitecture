using System.Linq.Expressions;

namespace Github.NetCoreWebApp.Core.Applications.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetByIdAsync(object id);
        Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task UpdateAsync(T updatedEntity, T oldEntity);
        void Remove(T entity);
        Task RemoveEager(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByFilterEager(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
    }
}
