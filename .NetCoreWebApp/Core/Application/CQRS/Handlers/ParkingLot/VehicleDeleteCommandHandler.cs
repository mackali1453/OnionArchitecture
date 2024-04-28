using Application.CQRS.Queries;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLot
{
    public class ParkingLotDeleteCommandHandler : IRequestHandler<ParkingLotDeleteCommandRequest, Unit>
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotDeleteCommandHandler(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        public async Task<Unit> Handle(ParkingLotDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            await _parkingLotService.Delete(request.Id);

            return Unit.Value;
        }
    }
}
