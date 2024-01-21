using MediatR;

namespace Application.Aggregates.Category.Commands
{
    public class UpdateCategoryCommandRequest : IRequest
    {
        public int Id { get; set; }
        public string? Definition { get; set; }

        public UpdateCategoryCommandRequest(int ıd, string? definition)
        {
            Id = ıd;
            Definition = definition;
        }
    }
}
