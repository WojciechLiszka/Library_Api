using Library_Api.Entity;
using Library_Api.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.Command
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly ILogger<DeleteBookCommandHandler> _logger;
        private readonly LibraryDbContext _dbContext;

        public DeleteBookCommandHandler( LibraryDbContext dbContext, ILogger<DeleteBookCommandHandler> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            _logger.LogError($"Book with id: {request.Id} DELETE action invoked");
            var book = await _dbContext
                .Books
                .FirstOrDefaultAsync(b => b.Id == request.Id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}