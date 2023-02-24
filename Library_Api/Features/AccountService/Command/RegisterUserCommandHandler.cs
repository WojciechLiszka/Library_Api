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
                Email = request.dto.Email,
                DateOfBirth = request.dto.DateOfBirth,
                Nationality = request.dto.Nationality,
                RoleId = request.dto.RoleId,
                FirstName = request.dto.FirstName,
                LastName = request.dto.LastName,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, request.dto.Password);

            newUser.PasswordHash = hashedPassword;
            await _dbContext.Users.AddAsync(newUser);
            _dbContext.SaveChanges();
            return Unit.Value;
        }
    }
}