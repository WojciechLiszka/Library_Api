using Library_Api.Entity;
using Library_Api.Exceptions;
using Library_Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.Query
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly LibraryDbContext _dbContext;

        public GetBookByIdQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _dbContext
                .Books
                .Include(b => b.Tags)
                .Select(Book => new BookDto()
                {
                    Id = Book.Id,
                    Tittle = Book.Tittle,
                    Author = Book.Author,
                    PublishDate = Book.PublishDate,
                    IsAvailable = Book.IsAvailable,
                    Tags = Book.Tags.Select(b => b.Name).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id);

            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            return book;
        }
    }
}