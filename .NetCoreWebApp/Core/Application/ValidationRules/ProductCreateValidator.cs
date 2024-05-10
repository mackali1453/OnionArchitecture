using Application.CQRS.Commands;
using FluentValidation;
namespace Github.NetCoreWebApp.Core.Application.ValidationRules
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateCommandRequest>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Name is required.").Must(name => !ContainsDigit(name)).WithMessage("Name cannot contain digits.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Stock must be greater than 0.");
        }
        private bool ContainsDigit(string value)
        {
            foreach (char c in value)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}