using Application.CQRS.Commands;
using FluentValidation;
namespace Github.NetCoreWebApp.Core.Application.ValidationRules
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateCommandRequest>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}