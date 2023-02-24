using Library_Api.Entity;
using Library_Api.Features.TagService.Command;
using Library_Api.Features.TagService.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/Tag")]
        public async Task<ActionResult> CreateTag([FromBody] string name)
        {
            var request = new CreateTagCommand()
            {
                Name = name
            };
            var result = await _mediator.Send(request);
            return Created($"/api/Tag/{result}", null);
        }

        [HttpPut("api/Book/{bookId}/Tag/{tagId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> AddTagToBook([FromRoute] int bookId, [FromRoute] int tagId)
        {
            var request = new AddTagToBookCommand()
            {
                BookId = bookId,
                TagId = tagId
            };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("api/Tag")]
        public async Task<ActionResult<List<Tag>>> GetAllTags()
        {
            var request = new GetAllTagsQuery();
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}