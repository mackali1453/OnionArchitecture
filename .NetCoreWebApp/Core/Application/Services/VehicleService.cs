using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using Github.NetCoreWebApp.Core.Application.Interfaces;

namespace Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IWebApiIuow _iUow;
        private readonly IMapper _mapper;
        private readonly IServiceLogger<VehicleService> _logger;
        public VehicleService(IWebApiIuow iUow, IMapper mapper, IServiceLogger<VehicleService> logger)
        {
            _iUow = iUow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VehicleResponseDto> GetById(int id)
        {
            var vehicle = new AppVehicle();

            try
            {
                var vehicleRepository = _iUow.GetRepository<AppVehicle>();

                vehicle = await vehicleRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(vehicle));
        }
        public async Task Delete(int id)
        {
            try
            {
                var vehicleRepository = _iUow.GetRepository<AppVehicle>();
                var vehicle = await vehicleRepository.GetByIdAsync(id);
                vehicleRepository.Remove(vehicle);
                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<VehicleResponseDto> Update(VehicleUpdateCommandRequest request)
        {
            var newVehicle = new AppVehicle(request.VehiclePlate, request.VehicleColor, request.VehicleModel, request.VehicleBrand, request.IsActive);

            try
            {
                var vehicleRepository = _iUow.GetRepository<AppVehicle>();
                var oldVehicle = await vehicleRepository.GetByIdAsync(request.VehicleId);

                await vehicleRepository.UpdateAsync(newVehicle, oldVehicle);

                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(newVehicle));
        }
        public async Task<VehicleResponseDto> Create(VehicleCreateCommandRequest request)
        {
            var newVehicle = new AppVehicle();

            try
            {
                var vehicleRepository = _iUow.GetRepository<AppVehicle>();
                var appUserRepository = _iUow.GetRepository<AppUser>();

                var user = await appUserRepository.GetByIdAsync(request.UserId);

                newVehicle = new AppVehicle(request.VehiclePlate, request.VehicleColor, request.VehicleModel, request.VehicleBrand, request.IsActive);
                newVehicle.SetRelationWithUser(user);

                await vehicleRepository.CreateAsync(newVehicle);

                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(newVehicle));
        }
    }
}
