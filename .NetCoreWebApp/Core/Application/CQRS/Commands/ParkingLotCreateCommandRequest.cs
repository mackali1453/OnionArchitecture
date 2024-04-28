using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class ParkingLotCreateCommandRequest: IRequest<ParkingLotResponseDto>
    {
        public long Latitude { get;set; }
        public long Longitude { get;set; }
        public int UserId { get; set; }
    }
}
