using Library_Api.Features.AccountService.Command;
using Library_Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RegisterUser(RegisterUserDto dto)
        {
            var request = new RegisterUserCommand()
            {
                dto = dto
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("Login")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var request = new LoginUserCommand()
            {
                dto = dto
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}