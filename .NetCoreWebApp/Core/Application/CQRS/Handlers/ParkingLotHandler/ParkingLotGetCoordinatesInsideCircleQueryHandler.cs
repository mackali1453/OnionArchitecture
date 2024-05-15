using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Common.Interfaces;
using Domain.Entities.Aggregates;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLotHandler
{
    public class ParkingLotGetCoordinatesInsideCircleQueryHandler : IRequestHandler<ParkingLotCoordinatesInsideCircleRequest, ParkingLotListResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public ParkingLotGetCoordinatesInsideCircleQueryHandler(IWebApiIuow unitOfWork, IMapper mapper, IUtility utility)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utility = utility;
        }

        public async Task<ParkingLotListResponseDto> Handle(ParkingLotCoordinatesInsideCircleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parkingLotsInsideCircle = new List<ParkingLot>();

                var parkingLotRepo = _unitOfWork.GetRepository<ParkingLot>();
                var allParkingLots = await parkingLotRepo.GetAll();

                foreach (var parkingLot in allParkingLots)
                {
                    if (_utility.IsCoordinateInsideCircle(parkingLot.Latitude, parkingLot.Longitude, request.CenterLatitude, request.CenterLongitude, request.Radius))
                    {
                        parkingLotsInsideCircle.Add(parkingLot);
                    }
                }

                return new ParkingLotListResponseDto(true, "", _mapper.Map<List<ParkingLotData>>(parkingLotsInsideCircle));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve parking lots inside circle.", ex);
            }
        }
    }
}
