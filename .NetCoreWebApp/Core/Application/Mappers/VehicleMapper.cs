using Application.Dto;
using AutoMapper;
using Domain.Entities.Aggregates;

namespace Application.Mappers
{
    public class VehicleMapper : Profile
    {
        public VehicleMapper()
        {
            CreateMap<AppVehicle, VehicleData>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.VehicleBrand, opt => opt.MapFrom(src => src.VehicleBrand))
                        .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.VehicleModel))
                        .ForMember(dest => dest.VehicleColor, opt => opt.MapFrom(src => src.VehicleColor))
                        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                        .ForMember(dest => dest.VehiclePlate, opt => opt.MapFrom(src => src.VehiclePlate)).ReverseMap();
        }
    }
}
