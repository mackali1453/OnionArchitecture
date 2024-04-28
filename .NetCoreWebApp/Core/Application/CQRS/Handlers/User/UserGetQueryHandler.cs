using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class UserGetQueryHandler : IRequestHandler<UserGetQueryRequest, UserResponseDto>
    {
        private readonly IUserService _userService;

        public UserGetQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponseDto> Handle(UserGetQueryRequest request, CancellationToken cancellationToken)
        {
            return await _userService.GetById(request.Id);
        }
    }
}
