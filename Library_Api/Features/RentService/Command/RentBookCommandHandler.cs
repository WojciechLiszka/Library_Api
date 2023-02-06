using Library_Api.Entity;
using Library_Api.Exceptions;
using Library_Api.Features.AdminPanel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.RentService.Command
{
    public class RentBookCommandHandler : IRequestHandler<RentBookCommand, Unit>
    {
        private readonly LibraryDbContext _dbContext;
        private readonly ApiConfiguration _configuration;

        public RentBookCommandHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = ApiConfiguration.GetInstance();
        }

        public async Task<Unit> Handle(RentBookCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == request.userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var book = await _dbContext
                .Books
                .FirstOrDefaultAsync(b => b.Id == request.bookId);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            if (book.IsAvailable == false)
            {
                throw new NotFoundException("Book is not Available");
            }
            var rent = new Rent()
            {
                UserId = request.userId,
                BookId = request.bookId,
                BookName = book.Tittle,
                Starts = DateTime.Now,
                Ends = DateTime.Now.AddDays(_configuration.RentDays),
                ReturnDate = null,
                Fee = 0
            };
            _dbContext.Rents.Add(rent);
            book.IsAvailable = false;
            _dbContext.SaveChanges();
            return Unit.Value;
        }
    }
}