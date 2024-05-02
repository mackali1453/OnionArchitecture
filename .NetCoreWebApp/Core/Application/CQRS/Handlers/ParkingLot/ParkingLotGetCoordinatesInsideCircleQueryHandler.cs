using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLot
{
    public class ParkingLotGetCoordinatesInsideCircleQueryHandler : IRequestHandler<ParkingLotCoordinatesInsideCircleRequest, ParkingLotListResponseDto>
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotGetCoordinatesInsideCircleQueryHandler(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        public async Task<ParkingLotListResponseDto> Handle(ParkingLotCoordinatesInsideCircleRequest request, CancellationToken cancellationToken)
        {
            return await _parkingLotService.GetCoordinatesInsideCircle(request);
        }
    }
}
