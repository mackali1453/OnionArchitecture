using Application.CQRS.Commands;
using Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Github.NetCoreWebApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await this._mediator.Send(new UserGetQueryRequest { Id = id });

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(UserCreateCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
        [HttpPut]
        [Route("Update")]
        
        public async Task<IActionResult> Update(UserUpdateCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
        [HttpDelete]
        [Route("Delete")]
        
        public async Task<IActionResult> Delete(UserDeleteCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
    }
}
