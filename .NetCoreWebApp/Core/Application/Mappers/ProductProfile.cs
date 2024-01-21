using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Dto;
using Github.NetCoreWebApp.Core.Domain.Entities;

namespace Github.NetCoreWebApp.Core.Application.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductListDto, Product>().ReverseMap();
        }
    }
}
