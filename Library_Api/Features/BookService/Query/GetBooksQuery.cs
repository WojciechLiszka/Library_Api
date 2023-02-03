using Library_Api.Entity;
using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.Query
{
    public class GetBooksQuery : IRequest<PagedResult<BookDto>>
    {
        public BookQuery query { get; set; }
    }
}