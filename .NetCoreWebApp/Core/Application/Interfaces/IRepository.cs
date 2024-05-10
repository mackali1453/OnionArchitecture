using System.Linq.Expressions;

namespace Github.NetCoreWebApp.Core.Applications.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAll();
        Task<T> GetByIdAsync(object id);
        Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task UpdateAsync(T updatedEntity, T oldEntity);
        Task UpdateAsync(IEnumerable<T> entities);
        void Remove(T entity);
        Task RemoveEager(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>?> GetByFilterEager(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
    }
}
