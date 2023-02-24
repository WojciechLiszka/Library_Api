using MediatR;

namespace Library_Api.Features.TagService.Command
{
    public class AddTagToBookCommand : IRequest
    {
        public int BookId { get; set; }
        public int TagId { get; set; }
    }
}