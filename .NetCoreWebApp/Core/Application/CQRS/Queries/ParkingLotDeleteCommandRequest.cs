using Application.Dto;
using MediatR;

namespace Application.CQRS.Queries
{
    public class ParkingLotDeleteCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
