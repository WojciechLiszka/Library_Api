using Library_Api.Entity;
using Library_Api.Exceptions;
using Library_Api.Features.RentService.Command.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.RentService.Command
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, double>
    {
        private readonly LibraryDbContext _dbContext;

        public ReturnBookCommandHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<double> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var rent = await _dbContext
                .Rents
                .FirstOrDefaultAsync(r => r.Id == request.RentId);
            if (rent == null)
            {
                throw new NotFoundException("Book not found");
            }
            if (rent.ReturnDate != null)
            {
                var date = rent.ReturnDate;
                throw new BadRequestException($"Book aready returned at: {date}");
            }
            rent.Fee = CalculateFee.Calculate(rent);
            rent.ReturnDate = DateTime.Now;
            var book = await _dbContext
                .Books
                .FirstOrDefaultAsync(b => b.Id == rent.BookId);
            if (book == null)
            {
                throw new SystemError("System Error invalid Rent data");
            }
            book.IsAvailable = true;
            return rent.Fee;
        }
    }
}