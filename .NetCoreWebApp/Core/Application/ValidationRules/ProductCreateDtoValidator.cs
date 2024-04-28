//using Application.CQRS.Commands;
//using FluentValidation;
//using System.Text.RegularExpressions;

//namespace Github.NetCoreWebApp.Core.Application.ValidationRules
//{
//    public class ProductCreateDtoValidator : GenericValidator<CreateProductCommandRequest>
//    {
//        public const int RequestType = 1;
//        public ProductCreateDtoValidator()
//        {
//            RuleFor(x => x.Stock).LessThan(100).WithMessage("Stok 100'den az olmalıdır!");
//        }

//        private bool OnlyInteger(string arg)
//        {
//            return Regex.IsMatch(arg, "^[0-9]*$");
//        }
//    }
//}
