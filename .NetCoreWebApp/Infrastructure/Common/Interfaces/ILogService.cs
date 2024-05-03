namespace Common.Interfaces
{
    public interface ILogService
    {
        Task Save(object message,string categoryName);
    }
}
