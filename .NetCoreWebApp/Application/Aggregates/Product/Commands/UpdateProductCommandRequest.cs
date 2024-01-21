using MediatR;

namespace Application.Aggregates.Product.Commands
{
    public class UpdateProductCommandRequest : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public UpdateProductCommandRequest(string? name, int stock, decimal price, int categoryId)
        {
            Name = name;
            Stock = stock;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
