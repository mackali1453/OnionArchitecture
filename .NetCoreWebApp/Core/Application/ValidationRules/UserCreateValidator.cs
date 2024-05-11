using Application.CQRS.Commands;
using FluentValidation;

namespace Application.ValidationRules
{
    public class UserCreateValidator : AbstractValidator<UserCreateCommandRequest>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                                       .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.")
                                    .MaximumLength(50).WithMessage("Surname cannot be longer than 50 characters.");

            RuleFor(x => x.MobilePhoneNumber).NotEmpty().WithMessage("Mobile phone number is required.")
                                              .MaximumLength(15).WithMessage("Mobile phone number cannot be longer than 15 characters.");

            RuleFor(x => x.Tckn.ToString()).NotEmpty().WithMessage("TCKN is required.")
                                            .Length(11).WithMessage("TCKN must be 11 characters long.")
                                            .Matches("^[0-9]*$").WithMessage("TCKN must contain only digits.");

            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.")
                                   .MaximumLength(10).WithMessage("Gender cannot be longer than 10 characters.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.")
                                      .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                      .MaximumLength(50).WithMessage("Password cannot be longer than 50 characters.");
        }
    }
}
