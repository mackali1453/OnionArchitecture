using Application.CQRS.Commands;
using Application.Dto;

namespace Application.Interfaces
{
    public interface IUserService : IBaseService<UserResponseDto, UserCreateCommandRequest, UserUpdateCommandRequest>
    {
    }
}
