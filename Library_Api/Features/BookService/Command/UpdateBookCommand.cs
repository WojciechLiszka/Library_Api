using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.Command
{
    public class UpdateBookCommand : IRequest
    {
        public CreateBookDto Dto { get; set; }
        public int Id { get; set; }
    }
}