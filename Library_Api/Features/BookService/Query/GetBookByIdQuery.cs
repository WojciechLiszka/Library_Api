using Library_Api.Entity;
using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.Query
{
    public class GetBookByIdQuery : IRequest<BookDto>
    {
        public int Id { get; set; }
    }
}