using MediatR;

namespace Application.Aggregates.Product.Commands
{
    public class DeleteProductCommandRequest : IRequest
    {
        public int Id { get; set; }

        public DeleteProductCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
