using Application.Dto;
using AutoMapper;
using Domain.Entities.Aggregates;

namespace Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserData, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.MobilePhoneNumber, opt => opt.MapFrom(src => src.MobilePhoneNumber))
                .ForMember(dest => dest.Tckn, opt => opt.MapFrom(src => src.Tckn))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.AppVehicle, opt => opt.MapFrom(src => src.AppVehicle))
                .ReverseMap()
                .ForMember(dest => dest.AppVehicle, opt => opt.MapFrom(src => src.AppVehicle));

        }
    }
}
