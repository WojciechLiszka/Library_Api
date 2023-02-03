using Library_Api.Entity;
using Library_Api.Features.BookService.Query;
using Library_Api.Features.Command;
using Library_Api.Features.Query;
using Library_Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks([FromQuery] BookQuery query)
        {
            var request = new GetBooksQuery()
            {
                query = query
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> CreateBook([FromBody] CreateBookDto dto)
        {
            var request = new CreateBookCommand()
            {
                Dto = dto
            };
            var id = await _mediator.Send(request);
            return Created($"api/Book/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> DeleteBook([FromRoute] int id)
        {
            var request = new DeleteBookCommand()
            {
                Id = id
            };
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> UpdateBook([FromRoute] int id, [FromBody] CreateBookDto dto)
        {
            var request = new UpdateBookCommand()
            {
                Id = id,
                Dto = dto
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById([FromRoute] int id)
        {
            var request = new GetBookByIdQuery()
            {
                Id = id
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("Tag/{tagId}")]
        public async Task<ActionResult<List<Book>>> GetBookByTag([FromRoute] int tagId)
        {
            var request = new GetBookByTagQuery()
            {
                TagId = tagId
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}