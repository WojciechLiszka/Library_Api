using Library_Api.Entity;
using Library_Api.Exceptions;
using Library_Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.RentService.Query
{
    public class GetUserRentsQueryHandler : IRequestHandler<GetUserRentsQuery, PagedResult<Rent>>
    {
        private readonly LibraryDbContext _dbContext;

        public GetUserRentsQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<Rent>> Handle(GetUserRentsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var rents = await _dbContext
                .Rents
                .AsNoTracking()
                .Skip(request.query.PageSize * (request.query.PageNumber - 1))
                .Take(request.query.PageSize)
                .Where(r => r.UserId == user.Id)
                .ToListAsync();
            var count = rents.Count();
            var result = new PagedResult<Rent>(rents, count, request.query.PageSize, request.query.PageNumber);
            return result;
        }
    }
}