using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLot
{
    public class ParkingLotUpdateCommandHandler : IRequestHandler<ParkingLotUpdateCommandRequest, ParkingLotResponseDto>
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotUpdateCommandHandler(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        public async Task<ParkingLotResponseDto> Handle(ParkingLotUpdateCommandRequest updateParkingLotRequest, CancellationToken cancellationToken)
        {
            return await _parkingLotService.Update(updateParkingLotRequest);
        }
    }
}
