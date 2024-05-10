using Application.CQRS.Commands;
using Application.CQRS.Handlers;
using Application.Dto;

namespace Application.Interfaces
{
    public interface IProductService  : IBaseService<ProductResponseDto, ProductCreateCommandRequest, ProductUpdateCommandRequest>
    {
    }
}
