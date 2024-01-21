using Application.Aggregates.User.Commands;
using Application.Aggregates.User.Queries;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Presentation.Middleware.Logging;
using Github.NetCoreWebApp.Presentation.Middleware.Validation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Github.NetCoreWebApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/User")]
    [ServiceFilter(typeof(LoggingAttribute))]

    public class UserController : ControllerBase
    {
        private readonly IServiceLogger<UserController> _logger;

        private readonly IMediator _mediator;

        public UserController(IServiceLogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Create(RegisterUserCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }

        [HttpPost]
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> SignIn(CheckUserQueryRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
        [HttpPost]
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }
    }
}
