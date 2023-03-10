using Library_Api.Features.RentService.Command;
using Library_Api.Features.RentService.Query;
using Library_Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [ApiController]
    [Route("/api/Rent")]
    public class RentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("{BookId}")]
        [Authorize(Roles = "Admin,Librarian")]
            [ProducesDefaultResponseType]
        public async Task<ActionResult> RentBook([FromRoute] int bookId, [FromBody] int UserId)
        {
            var request = new RentBookCommand()
            {
                BookId = bookId,
                UserId = UserId
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRents([FromQuery] RentQuery query)
        {
            var request = new GetRentsQuery()
            {
                query = query
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("User/{id}")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetUserRents([FromRoute] int id, [FromQuery] RentQuery query)
        {
            var reqest = new GetUserRentsQuery()
            {
                userId = id,
                query = query
            };
            var result = await _mediator.Send(reqest);
            return Ok(result);
        }
        [HttpPut]
        [Route("{rentId}")]
        [Authorize(Roles = "Admin,Librarian")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ReturnBook([FromRoute] int rentId)
        {
            var reqest = new ReturnBookCommand()
            {
                RentId = rentId
            };
            var result = await _mediator.Send(reqest);
            return Ok($"Fee for rent : {result}");
        }
    }
}