using MediatR;

namespace Application.Aggregates.Category.Commands
{
    public class CreateCategoryCommandRequest : IRequest
    {
        public string? Definition { get; set; }

        public CreateCategoryCommandRequest(string? definition)
        {
            Definition = definition;
        }
    }
}
