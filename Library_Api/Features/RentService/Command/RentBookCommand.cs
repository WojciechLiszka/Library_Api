using MediatR;

namespace Library_Api.Features.RentService.Command
{
    public class RentBookCommand : IRequest
    {
        public int bookId { get; set; }
        public int userId { get; set; }
    }
}