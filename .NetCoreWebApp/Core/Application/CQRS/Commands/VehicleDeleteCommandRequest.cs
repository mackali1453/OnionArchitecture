using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class VehicleDeleteCommandRequest : IRequest
    {
        public int Id { get; set; }
    }
}
