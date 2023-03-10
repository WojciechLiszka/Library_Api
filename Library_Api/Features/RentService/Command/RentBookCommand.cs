using MediatR;

namespace Library_Api.Features.RentService.Command
{
    public class RentBookCommand : IRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}