using Library_Api.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.RentService.Query
{
    public class GetAllRentsQueryHandler : IRequestHandler<GetAllRentsQuery, List<Rent>>
    {
        private readonly LibraryDbContext _dbContext;

        public GetAllRentsQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Rent>> Handle(GetAllRentsQuery request, CancellationToken cancellationToken)
        {
            var rents = await _dbContext
                .Rents
                .AsNoTracking()
                .ToListAsync();

            return rents;
        }
    }
}