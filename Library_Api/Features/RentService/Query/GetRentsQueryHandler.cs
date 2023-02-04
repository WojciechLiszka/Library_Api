using Library_Api.Entity;
using Library_Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.RentService.Query
{
    public class GetRentsQueryHandler : IRequestHandler<GetRentsQuery, PagedResult<Rent>>
    {
        private readonly LibraryDbContext _dbContext;

        public GetRentsQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<Rent>> Handle(GetRentsQuery request, CancellationToken cancellationToken)
        {
            var rents = await _dbContext
                .Rents
                .Skip(request.query.PageSize * (request.query.PageNumber - 1))
                .Take(request.query.PageSize)
                .AsNoTracking()
                .ToListAsync();
            var count = rents.Count();
            var result = new PagedResult<Rent>(rents, count, request.query.PageSize, request.query.PageNumber);

            return result;
        }
    }
}