using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;

namespace Application.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public ParkingLotService(IWebApiIuow unitOfWork, IMapper mapper, IUtility utility)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _utility = utility ?? throw new ArgumentNullException(nameof(utility));
        }

        public async Task<ParkingLotListResponseDto> GetCoordinatesInsideCircle(ParkingLotCoordinatesInsideCircleRequest request)
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

        public async Task<ParkingLotResponseDto> GetById(int id)
        {
            try
            {
                var parkingLotRepository = _unitOfWork.GetRepository<ParkingLot>();
                var parkingLot = await parkingLotRepository.GetByIdAsync(id);

                if (parkingLot == null)
                {
                    return new ParkingLotResponseDto(true, $"Parking lot with ID {id} not found.", null);
                }

                return new ParkingLotResponseDto(true, "", _mapper.Map<ParkingLotData>(parkingLot));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve parking lot with ID {id}.", ex);
            }
        }

        public async Task<ParkingLotResponseDto> Update(ParkingLotUpdateCommandRequest updateParkingLotRequest)
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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingLotResponseDto> Create(object request)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingLotResponseDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
