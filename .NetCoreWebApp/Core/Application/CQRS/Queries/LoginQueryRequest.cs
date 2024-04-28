using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;

namespace Application.CQRS.Queries
{
    public class LoginQueryRequest : IRequest<LoginResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
