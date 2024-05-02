using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class ParkingLotUpdateCommandRequest: IRequest<ParkingLotResponseDto>
    {
        public double Latitude { get;set; }
        public double Longitude { get;set; }
        public int Id { get; set; }
    }
}
