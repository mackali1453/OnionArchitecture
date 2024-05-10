using Application.CQRS.Queries;
using FluentValidation;
namespace Github.NetCoreWebApp.Core.Application.ValidationRules
{
    public class ProductGetValidator : AbstractValidator<ProductGetQueryRequest>
    {
        public ProductGetValidator()
        {
            When(x => x.Price != 0, () =>
            {
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            });
            When(x => x.Stock != 0, () =>
            {
                RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Stock must be greater than 0.");
            });
        }
    }
}