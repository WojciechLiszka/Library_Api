using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.RentService.Command
{
    public class ReturnBookCommand:IRequest<double>
    {
        public int RentId { get; set; }
    }
}
