namespace Application.Interfaces
{
    public interface IResponse<T>
    {
        bool Success { get; }
        string Message { get; }
        T Data { get; }
    }
}
