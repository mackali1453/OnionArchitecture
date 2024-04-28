using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLot
{
    public class ParkingLotCreateCommandHandler : IRequestHandler<ParkingLotCreateCommandRequest, ParkingLotResponseDto>
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotCreateCommandHandler(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        public async Task<ParkingLotResponseDto> Handle(ParkingLotCreateCommandRequest request, CancellationToken cancellationToken)
        {
            return await _parkingLotService.Create(request);
        }
    }
}
