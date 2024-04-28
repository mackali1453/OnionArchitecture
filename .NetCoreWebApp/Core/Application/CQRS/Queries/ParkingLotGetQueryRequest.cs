using Application.Dto;
using MediatR;

namespace Application.CQRS.Queries
{
    public class ParkingLotGetQueryRequest : IRequest<ParkingLotResponseDto>
    {
        public int Id { get; set; }
    }
}
