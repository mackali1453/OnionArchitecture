using Application.CQRS.Commands;
using Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Github.NetCoreWebApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ParkingLotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParkingLotController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await this._mediator.Send(new ParkingLotGetQueryRequest { Id = id });

            return Ok(result);
        }
        [HttpPost]
        [Route("GetCoordinatesInsideCircle")]
        public async Task<IActionResult> GetCoordinatesInsideCircle(ParkingLotCoordinatesInsideCircleRequest request)
        {
            var result = await this._mediator.Send(request);

            return Ok(result);
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(ParkingLotUpdateCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
    }
}
