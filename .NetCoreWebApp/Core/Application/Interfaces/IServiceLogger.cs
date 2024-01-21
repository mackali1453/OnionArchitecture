namespace Github.NetCoreWebApp.Core.Applications.Interfaces
{
    public interface IServiceLogger<in T> where T : class
    {
        void Info(object message);
        void Error(object message);

        void Save(object message);
    }
}