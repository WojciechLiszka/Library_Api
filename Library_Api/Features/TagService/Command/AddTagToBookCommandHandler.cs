using Library_Api.Entity;
using Library_Api.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.TagService.Command
{
    public class AddTagToBookCommandHandler : IRequestHandler<AddTagToBookCommand, Unit>
    {
        private readonly LibraryDbContext _dbContext;

        public AddTagToBookCommandHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddTagToBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext
                .Books
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id == request.BookId);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            var tag = await _dbContext
                .Tags
                .FirstOrDefaultAsync(t => t.Id == request.TagId);
            if (tag == null)
            {
                throw new NotFoundException("Tag not found");
            }
            book.Tags.Add(tag);
            _dbContext.SaveChanges();
            return Unit.Value;
        }
    }
}