using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class ParkingLotUpdateCommandRequest: IRequest<ParkingLotResponseDto>
    {
        public long Latitude { get;set; }
        public long Longitude { get;set; }
        public int Id { get; set; }
    }
}
