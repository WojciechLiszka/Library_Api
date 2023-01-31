using Library_Api.Features.RentService.Command;
using Library_Api.Features.RentService.Query;
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
        public async Task<ActionResult> RentBook([FromRoute] int BookId, [FromBody] int UserId)
        {
            var request = new RentBookCommand()
            {
                bookId = BookId,
                userId = UserId
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllRents()
        {
            var request = new GetAllRentsQuery();
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{rentId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> ReturnBook([FromRoute]int rentId)
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