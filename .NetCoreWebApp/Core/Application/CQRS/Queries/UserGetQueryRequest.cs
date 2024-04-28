using Application.Dto;
using MediatR;

namespace Application.CQRS.Queries
{
    public class UserGetQueryRequest : IRequest<UserResponseDto>
    {
        public int Id { get; set; }
    }
}
