using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.AccountService.Command
{
    public class RegisterUserCommand : IRequest
    {
        public RegisterUserDto Dto { get; set; }
    }
}