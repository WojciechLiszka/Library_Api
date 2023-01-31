using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.Command
{
    public class CreateBookCommand : IRequest<int>
    {
        public CreateBookDto Dto { get; set; }
    }
}