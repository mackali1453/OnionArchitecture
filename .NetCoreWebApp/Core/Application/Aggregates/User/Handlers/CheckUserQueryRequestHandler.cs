using Application.Aggregates.User.Queries;
using Github.NetCoreWebApp.Core.Application.Services;
using Github.NetCoreWebApp.Core.Applications.Dto;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using MediatR;

namespace Application.Aggregates.User.Handlers
{
    public class CheckUserQueryRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IUow _iUow;

        public CheckUserQueryRequestHandler(IUow iUow)
        {
            _iUow = iUow;
        }

        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var responseDto = new CheckUserResponseDto();
            var userRepo = _iUow.GetUserRepository();
            var user = userRepo.GetEagerUsers(request.Username);

            if (user != null && user.Password == request.Password)
            {
                responseDto.AccessToken = new JwtService().GenerateJwtToken(user.AppUserRole.Select(x => x.AppRoles).Select(x => x.RoleName).ToArray());
            }
            return responseDto;
        }
    }
}
