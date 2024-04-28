using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommandRequest, UserResponseDto>
    {
        private readonly IUserService _userService;

        public UserUpdateCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponseDto> Handle(UserUpdateCommandRequest updateUserRequest, CancellationToken cancellationToken)
        {
            return await _userService.Update(updateUserRequest);
        }
    }
}
