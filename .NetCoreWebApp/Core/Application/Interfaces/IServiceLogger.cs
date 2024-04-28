namespace Github.NetCoreWebApp.Core.Application.Interfaces
{
    public interface IServiceLogger<in T> where T : class
    {
        Task Info(object message);
        Task Error(object message);

        Task Save(object message);
    }
}