using FluentValidation;
namespace Github.NetCoreWebApp.Core.Application.ValidationRules
{
    public class GenericValidator<T> : AbstractValidator<T>
    {
        public GenericValidator()
        {
            // Define your validation rules here using Fluent Validation syntax
            // Example rule: Require a non-null value for a property
            RuleFor(x => x).NotNull();
        }
    }
}