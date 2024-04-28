using Application.CQRS.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommandRequest, Unit>
    {
        private readonly IUserService _userService;

        public UserDeleteCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(UserDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.Delete(request.Id);

            return Unit.Value;
        }
    }
}
