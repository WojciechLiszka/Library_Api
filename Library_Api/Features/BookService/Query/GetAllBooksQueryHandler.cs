using AutoMapper;
using Library_Api.Entity;
using Library_Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.Query
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery,List<BookDto>>
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(LibraryDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        

        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var Books = await _dbContext
                .Books
                .Include(b=>b.Tags)
                . Select(Book => new BookDto()
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
            

            return Books;
        }
    }
}