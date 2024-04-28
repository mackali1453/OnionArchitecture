using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using Github.NetCoreWebApp.Core.Application.Interfaces;

namespace Application.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly IWebApiIuow _iUow;
        private readonly IMapper _mapper;
        private readonly IServiceLogger<ParkingLotService> _logger;

        public ParkingLotService(IWebApiIuow iUow, IMapper mapper, IServiceLogger<ParkingLotService> logger)
        {
            _iUow = iUow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ParkingLotResponseDto> GetById(int id)
        {
            var parkingLot = new ParkingLot();

            try
            {
                var parkingLotRepository = _iUow.GetRepository<ParkingLot>();
                parkingLot = await parkingLotRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new ParkingLotResponseDto(true, "", _mapper.Map<ParkingLotData>(parkingLot));
        }
        public async Task Delete(int id)
        {
            try
            {
                var parkingLotRepository = _iUow.GetRepository<ParkingLot>();
                var parkingLot = await parkingLotRepository.GetByIdAsync(id);
                parkingLotRepository.Remove(parkingLot);
                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ParkingLotResponseDto> Update(ParkingLotUpdateCommandRequest updateParkingLotRequest)
        {
            var newParkingLot = new ParkingLot(updateParkingLotRequest.Latitude, updateParkingLotRequest.Longitude);

            try
            {
                var parkingLotRepository = _iUow.GetRepository<ParkingLot>();

                var parkingLot = await parkingLotRepository.GetByIdAsync(updateParkingLotRequest.Id);

                await parkingLotRepository.UpdateAsync(newParkingLot, parkingLot);

                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new ParkingLotResponseDto(true, "", _mapper.Map<ParkingLotData>(newParkingLot));
        }
        public async Task<ParkingLotResponseDto> Create(ParkingLotCreateCommandRequest request)
        {
            var newParkingLot = new ParkingLot(request.Latitude, request.Longitude);

            try
            {
                var parkingLotRepository = _iUow.GetRepository<ParkingLot>();
                var appUserRepository = _iUow.GetRepository<AppUser>();

                var user = await appUserRepository.GetByIdAsync(request.UserId);
                newParkingLot.SetRelationWithUser(user);

                await parkingLotRepository.CreateAsync(newParkingLot);

                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new ParkingLotResponseDto(true, "", _mapper.Map<ParkingLotData>(newParkingLot));
        }
    }
}
