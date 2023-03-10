using Library_Api.Features.AdminPanel.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controller
{
    [ApiController]
    [Route("api/Account")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut]
        [Route("LateFee")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetLateTimeFee([FromBody] double fee)
        {
            var request = new SetLateTimeFeeCommand()
            {
                Fee = fee
            };
            await _mediator.Send(request);
            return Ok(request);
        }
        [HttpPut]
        [Route("RentTime")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RentTime([FromBody] int days)
        {
            var request = new SetRentTimeCommand()
            {
                Days = days
            };
            await _mediator.Send(request);
            return Ok();
        }
    }
}