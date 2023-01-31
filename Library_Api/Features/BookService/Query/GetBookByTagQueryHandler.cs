using Library_Api.Entity;
using Library_Api.Exceptions;
using Library_Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.BookService.Query
{
    public class GetBookByTagQueryHandler : IRequestHandler<GetBookByTagQuery, List<BookDto>>
    {
        private readonly LibraryDbContext _dbContext;

        public GetBookByTagQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<BookDto>> Handle(GetBookByTagQuery request, CancellationToken cancellationToken)
        {
            var tag= await _dbContext
                .Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(t=>t.Id==request.TagId);
            if (tag==null)
            {
                throw new NotFoundException("Tag not found");
            }
            var books = await _dbContext
                .Books
                .Include(b => b.Tags)
                .Where(b => b.Tags.Any(t => t.Name == tag.Name))
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
                .ToListAsync();
            return books;
        }
    }
}
