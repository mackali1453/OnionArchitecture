using Application.CQRS.Queries;
using Application.Interfaces;
using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class LoginQueryRequestHandler : IRequestHandler<LoginQueryRequest, LoginResponseDto>
    {
        private readonly IAuthService _authService;
        public LoginQueryRequestHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResponseDto> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {

            return await _authService.Authenticate(request.Password, request.Username);
        }
    }
}
