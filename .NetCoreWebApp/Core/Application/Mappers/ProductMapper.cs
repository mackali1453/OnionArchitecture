using Application.Dto;
using AutoMapper;
using Domain.Entities.Aggregates;

namespace Application.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductData, Product>()
                        .ForMember(dest => dest.PriceChange, opt => opt.MapFrom(src => src.PriceChange))
                        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                        .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                        .ForMember(dest => dest.PriceChange, opt => opt.MapFrom(src => src.PriceChange))
                        .ReverseMap();

            CreateMap<Domain.Entities.Aggregates.PriceChange, Application.Dto.PriceChange>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.ChangeDate));
        }
    }
}
