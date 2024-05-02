using Application.Dto;
using AutoMapper;
using Domain.Entities.Aggregates;

namespace Application.Mappers
{
    public class ParkingLotMapper : Profile
    {
        public ParkingLotMapper()
        {
            CreateMap<ParkingLot, ParkingLotData>()
                        .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                        .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                        .ReverseMap();
        }
    }
}
