// ValidateModelAttribute.cs
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Github.NetCoreWebApp.Presentation.Middleware.Logging
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LoggingAttribute : Attribute, IActionFilter
    {
        private readonly IServiceLogger<LoggingAttribute> _logger;

        public LoggingAttribute(IServiceLogger<LoggingAttribute> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requestString = string.Empty;

            try
            {
                if (context.ActionArguments.TryGetValue("model", out var model))
                {
                    var controllerName = context.RouteData.Values["controller"].ToString();

                    requestString = JsonConvert.SerializeObject(new { actionName = controllerName, request = model });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                _logger.Error(requestString);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var responseString = string.Empty;

            try
            {
                if (context.Result is ObjectResult objectResult)
                {
                    var controllerName = context.RouteData.Values["controller"].ToString();

                    responseString = JsonConvert.SerializeObject(new { actionName = controllerName, respnose = responseString });
                    if (context.Exception != null)
                    {
                        _logger.Error(context?.Exception);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                _logger.Error(responseString);
            }
        }
    }
}