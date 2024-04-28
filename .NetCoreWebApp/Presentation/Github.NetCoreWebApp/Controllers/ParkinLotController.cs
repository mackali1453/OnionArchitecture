using Application.CQRS.Commands;
using Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Github.NetCoreWebApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ParkingLotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParkingLotController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(ParkingLotGetQueryRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ParkingLotCreateCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(ParkingLotUpdateCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(ParkingLotDeleteCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
    }
}
