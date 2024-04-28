using FluentValidation;
using Github.NetCoreWebApp.Core.Application.Interfaces;
using Github.NetCoreWebApp.Core.Application.ValidationRules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Github.NetCoreWebApp.Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateModelAttribute : Attribute, IActionFilter
    {
        private readonly IServiceLogger<ValidateModelAttribute> _logger;

        public ValidateModelAttribute(IServiceLogger<ValidateModelAttribute> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("model", out var model))
            {
                await GetInstance(context, model);
            }
        }

        private async Task GetInstance(ActionExecutingContext context, object? model)
        {
            try
            {
                var validatorType = typeof(GenericValidator<>).MakeGenericType(model.GetType());

                List<Type> allSubTypes = new List<Type>();

                foreach (var assem in AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.Location.Contains("NetCoreWebApp")).ExportedTypes)
                {
                    //var subTypes = assem.GetGenericTypeDefinition()
                    if (assem.BaseType == validatorType)
                        allSubTypes.Add(assem);
                }

                var providerInstance = (IValidator)Activator.CreateInstance(allSubTypes.FirstOrDefault());

                var validationModel = new ValidationContext<object>(model);

                var validationResult = providerInstance.Validate(validationModel);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }

                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
            catch (Exception ex)
            {
                await _logger.Error(ex);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //LogginMiddleware
        }
    }
}