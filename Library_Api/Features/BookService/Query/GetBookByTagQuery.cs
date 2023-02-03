using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.BookService.Query
{
    public class GetBookByTagQuery : IRequest<PagedResult<BookDto>>
    {
        public BookQuery query { get; set; }
        public int TagId { get; set; }
    }
}