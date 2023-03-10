using Library_Api.Entity;
using MediatR;

namespace Library_Api.Features.Command
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly LibraryDbContext _dbContext;

        public CreateBookCommandHandler( LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book()
            {
                Tittle = request.Dto.Tittle,
                Author = request.Dto.Author,
                PublishDate = request.Dto.PublishDate
            };
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return book.Id;
        }
    }
}