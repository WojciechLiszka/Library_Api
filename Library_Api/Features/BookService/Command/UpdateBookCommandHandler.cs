using Library_Api.Entity;
using Library_Api.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.Command
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly LibraryDbContext _dbContext;

        public UpdateBookCommandHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext
                .Books
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            book.Author = request.Dto.Author;
            book.PublishDate = request.Dto.PublishDate;
            book.Tittle = request.Dto.Tittle;
            await _dbContext.SaveChangesAsync();
           
            return (Unit.Value);
        }
    }
}