using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLot
{
    public class ParkingLotGetQueryHandler : IRequestHandler<ParkingLotGetQueryRequest, ParkingLotResponseDto>
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotGetQueryHandler(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        public async Task<ParkingLotResponseDto> Handle(ParkingLotGetQueryRequest request, CancellationToken cancellationToken)
        {
            return await _parkingLotService.GetById(request.Id);
        }
    }
}
