using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.AccountService.Command
{
    public class LoginUserCommand : IRequest<string>
    {
        public LoginDto Dto { get; set; }
    }
}