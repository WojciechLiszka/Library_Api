using Library_Api.Entity;
using Library_Api.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Features.TagService.Command
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly LibraryDbContext _dbContext;

        public CreateTagCommandHandler(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _dbContext
                .Tags
                .FirstOrDefaultAsync(t => t.Name == request.Name);
            if (tag != null)
            {
                throw new BadRequestException("Tag must have unique name");
            }
            tag = new Tag()
            {
                Name = request.Name,
            };
            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();
            return tag.Id;
        }
    }
}