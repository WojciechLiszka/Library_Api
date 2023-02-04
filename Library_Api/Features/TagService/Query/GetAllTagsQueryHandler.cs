using Library_Api.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.TagService.Query
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<Tag>>
    {
        private readonly LibraryDbContext _dbContext;

        public GetAllTagsQueryHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var Tags = await _dbContext
                .Tags
                .AsNoTracking()
                .ToListAsync();
            return Tags;
        }
    }
}