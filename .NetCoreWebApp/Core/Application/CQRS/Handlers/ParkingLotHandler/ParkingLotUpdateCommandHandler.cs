using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using MediatR;

namespace Application.CQRS.Handlers.ParkingLotHandler
{
    public class ParkingLotUpdateCommandHandler : IRequestHandler<ParkingLotUpdateCommandRequest, ParkingLotResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public ParkingLotUpdateCommandHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ParkingLotResponseDto> Handle(ParkingLotUpdateCommandRequest updateParkingLotRequest, CancellationToken cancellationToken)
        {
            try
            {
                var parkingLotRepository = _unitOfWork.GetRepository<ParkingLot>();
                var parkingLot = await parkingLotRepository.GetByIdAsync(updateParkingLotRequest.Id);

                if (parkingLot == null)
                {
                    return new ParkingLotResponseDto(true, $"Parking lot with ID {updateParkingLotRequest.Id} not found.", null);
                }

                var newParkingLot = _mapper.Map<ParkingLot>(updateParkingLotRequest);
                newParkingLot.AddUser(parkingLot.AppUserId);
                await parkingLotRepository.UpdateAsync(newParkingLot, parkingLot);
                await _unitOfWork.SaveChangesAsync();

                return new ParkingLotResponseDto(true, "", _mapper.Map<ParkingLotData>(newParkingLot));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update parking lot.", ex);
            }
        }
    }
}
