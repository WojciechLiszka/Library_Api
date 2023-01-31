using Library_Api.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.RentService.Command
{
    public class RentBookCommand:IRequest
    {
        public int bookId { get; set; }
        public int userId { get; set; }
    }
}
