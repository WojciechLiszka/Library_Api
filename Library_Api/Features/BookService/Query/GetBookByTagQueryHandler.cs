using Library_Api.Entity;
using Library_Api.Exceptions;
using Library_Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.BookService.Query
{
    public class GetBookByTagQueryHandler : IRequestHandler<GetBookByTagQuery, PagedResult<BookDto>>
    {
        private readonly LibraryDbContext _dbContext;

        public GetBookByTagQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedResult<BookDto>> Handle(GetBookByTagQuery request, CancellationToken cancellationToken)
        {
            var tag= await _dbContext
                .Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(t=>t.Id==request.TagId);
            if (tag==null)
            {
                throw new NotFoundException("Tag not found");
            }
            var baseQuery = _dbContext
               .Books
               .Include(b => b.Tags)
               .Where(b => request.query.SearchPhrase == null
               || (b.Tittle.ToLower().Contains(request.query.SearchPhrase.ToLower())
               || b.Author.ToLower()
               .Contains(request.query.SearchPhrase.ToLower())))
               .Where(b=>b.Tags.Any(t=>t.Name==tag.Name));
            if (!string.IsNullOrEmpty(request.query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Book, object>>>
                {
                    { nameof(Book.Tittle), b => b.Tittle },
                    { nameof(Book.Author), b => b.Author },

                };

                var selectedColumn = columnsSelectors[request.query.SortBy];

                baseQuery = request.query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }
            var BooksDtos = await baseQuery
                .Skip(request.query.PageSize * (request.query.PageNumber - 1))
                .Take(request.query.PageSize)
                .Select(Book => new BookDto()
                {
                    Id = Book.Id,
                    Tittle = Book.Tittle,
                    Author = Book.Author,
                    PublishDate = Book.PublishDate,
                    IsAvailable = Book.IsAvailable,
                    Tags = Book.Tags.Select(b => b.Name).ToList()
                })
                .ToListAsync();
            var totalItemsCount = baseQuery.Count();
            var result = new PagedResult<BookDto>(BooksDtos, totalItemsCount, request.query.PageSize, request.query.PageNumber);


            return result;
        }
    }
}
