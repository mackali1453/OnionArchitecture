using MediatR;

namespace Application.Aggregates.Category.Commands
{
    public class DeleteCategoryCommandRequest : IRequest
    {
        public int Id { get; set; }

        public DeleteCategoryCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
