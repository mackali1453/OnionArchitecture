namespace Github.NetCoreWebApp.Core.Applications.Interfaces
{
    public interface IUow
    {
        Task SaveChangesAsync();
        IRepository<T> GetRepository<T>() where T : class, new();
    }
}
