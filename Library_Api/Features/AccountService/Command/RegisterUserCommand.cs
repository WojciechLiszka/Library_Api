using Library_Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.AccountService.Command
{
    public class RegisterUserCommand:IRequest
    {
        public RegisterUserDto dto { get; set; }
    }
}
