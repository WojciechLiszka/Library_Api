using Library_Api.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Library_Api.Features.AccountService.Command
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserCommandHandler(LibraryDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User()
            {
                Email = request.Dto.Email,
                DateOfBirth = request.Dto.DateOfBirth,
                Nationality = request.Dto.Nationality,
                RoleId = request.Dto.RoleId,
                FirstName = request.Dto.FirstName,
                LastName = request.Dto.LastName,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, request.Dto.Password);

            newUser.PasswordHash = hashedPassword;
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}