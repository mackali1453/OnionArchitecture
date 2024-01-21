using System.Linq.Expressions;

namespace Github.NetCoreWebApp.Core.Applications.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<T> GetByFilter(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task UpdateAsync(T updatedEntity, T oldEntity);
        Task RemoveAsyn(T entity);
    }
}
