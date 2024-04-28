using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, UserResponseDto>
    {
        private readonly IUserService _userService;

        public UserCreateCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponseDto> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            return await _userService.Create(request);
        }
    }
}
