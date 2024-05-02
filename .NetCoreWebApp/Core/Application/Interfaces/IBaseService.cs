namespace Application.Interfaces
{
    public interface IBaseService<TResponseDto, TCreateCommandRequest, TUpdateCommandRequest>
    {
        Task<TResponseDto> GetById(int id);
        Task Delete(int id);
        Task<TResponseDto> Update(TUpdateCommandRequest request);
        Task<TResponseDto> Create(TCreateCommandRequest request);
        Task<TResponseDto> GetAll();
    }
}
