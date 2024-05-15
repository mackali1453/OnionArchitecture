using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Common.Interfaces;
using Domain.Entities.Aggregates;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLotHandler
{
    public class ParkingLotGetQueryHandler : IRequestHandler<ParkingLotGetQueryRequest, ParkingLotResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public ParkingLotGetQueryHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ParkingLotResponseDto> Handle(ParkingLotGetQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parkingLotRepository = _unitOfWork.GetRepository<ParkingLot>();
                var parkingLot = await parkingLotRepository.GetByIdAsync(request.Id);

                if (parkingLot == null)
                {
                    return new ParkingLotResponseDto(true, $"Parking lot with ID {request.Id} not found.", null);
                }

                return new ParkingLotResponseDto(true, "", _mapper.Map<ParkingLotData>(parkingLot));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve parking lot with ID {request.Id}.", ex);
            }
        }
    }
}
