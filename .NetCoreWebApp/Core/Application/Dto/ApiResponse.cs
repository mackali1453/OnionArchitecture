using Application.Interfaces;

namespace Application.Dto
{
    public class ApiResponse<T> : IResponse<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }

        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

}
