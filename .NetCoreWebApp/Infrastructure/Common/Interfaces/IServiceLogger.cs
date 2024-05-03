namespace Github.NetCoreWebApp.Infrastructure.Common.Interfaces
{
    public interface IServiceLogger<in T> where T : class
    {
        Task Info(object message);
        Task Error(object message);
    }
}