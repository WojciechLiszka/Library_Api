using AutoMapper;
using Library_Api.Entity;
using MediatR;

namespace Library_Api.Features.Command
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _dbContext;

        public CreateBookCommandHandler(IMapper mapper, LibraryDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request.Dto);
            await _dbContext.Books.AddAsync(book);
            _dbContext.SaveChanges();
            return book.Id;
        }
    }
}