using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class ProductCreateCommandRequest : IRequest<ProductResponseDto>
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

    }
}
