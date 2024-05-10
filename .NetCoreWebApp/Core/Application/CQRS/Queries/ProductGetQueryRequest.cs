using Application.Dto;
using MediatR;

namespace Application.CQRS.Queries
{
    public class ProductGetQueryRequest : IRequest<ProductResponseDto>
    {
        public double Price { get; set; }
        public int Stock { get; set; }
    } 
}
