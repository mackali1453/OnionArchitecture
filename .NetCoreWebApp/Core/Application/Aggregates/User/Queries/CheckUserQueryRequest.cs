using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;

namespace Application.Aggregates.User.Queries
{
    public class CheckUserQueryRequest:IRequest<CheckUserResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
