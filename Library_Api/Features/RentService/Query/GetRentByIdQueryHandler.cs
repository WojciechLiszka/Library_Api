using Library_Api.Entity;
using Library_Api.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.RentService.Query
{
    public class GetRentByIdQueryHandler : IRequestHandler<GetRentByIdQuery, Rent>
    {
        private readonly LibraryDbContext _dbContext;

        public GetRentByIdQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Rent> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
        {
            var rent = await _dbContext
                .Rents
                .FirstOrDefaultAsync(r => r.Id == request.rentId);
            if(rent == null)
            {
                throw new NotFoundException("Rent not found");
            }
            return rent;
        }
    }
}