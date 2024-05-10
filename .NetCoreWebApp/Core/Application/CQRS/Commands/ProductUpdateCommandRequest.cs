using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class ProductUpdateCommandRequest : IRequest<ProductResponseDto>
    {
        public int ProductID { get; set; }
        public int Price { get; set; }
    }
}
