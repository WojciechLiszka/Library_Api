using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.BookService.Query
{
    public class GetBookByTagQuery : IRequest<List<BookDto>>
    {
        public int TagId { get; set; }
    }
}