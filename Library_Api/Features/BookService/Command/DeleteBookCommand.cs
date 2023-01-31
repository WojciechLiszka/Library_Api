using MediatR;

namespace Library_Api.Features.Command
{
    public class DeleteBookCommand : IRequest
    {
        public int Id { get; set; }
    }
}