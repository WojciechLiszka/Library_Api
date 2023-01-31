using Library_Api.Entity;
using Library_Api.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.RentService.Query
{
    public class GetUserRentsQueryHandler : IRequestHandler<GetUserRentsQuery, List<Rent>>
    {
        private readonly LibraryDbContext _dbContext;

        public GetUserRentsQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Rent>> Handle(GetUserRentsQuery request, CancellationToken cancellationToken)
        {
            var user =await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var Rents = await _dbContext
                .Rents
                .AsNoTracking()
                .Where(r => r.UserId == user.Id)
                .ToListAsync();
            return Rents;
        }
    }
}