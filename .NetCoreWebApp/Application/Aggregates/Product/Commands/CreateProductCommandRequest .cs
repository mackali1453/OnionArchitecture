using MediatR;

namespace Application.Aggregates.Product.Commands
{
    public class CreateProductCommandRequest : IRequest
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public CreateProductCommandRequest(string? name, int stock, decimal price, int categoryId)
        {
            Name = name;
            Stock = stock;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
