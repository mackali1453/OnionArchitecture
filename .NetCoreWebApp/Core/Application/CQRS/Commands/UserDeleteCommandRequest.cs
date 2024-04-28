using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class UserDeleteCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
