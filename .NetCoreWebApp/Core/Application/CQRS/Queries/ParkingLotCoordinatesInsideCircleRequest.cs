using Application.Dto;
using MediatR;

namespace Application.CQRS.Queries
{
    public class ParkingLotCoordinatesInsideCircleRequest : IRequest<ParkingLotListResponseDto>
    {
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }
        public double Radius { get; set; }
    }
}
