using Library_Api.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.RentService.Query
{
    public class GetRentByIdQuery:IRequest<Rent>
    {
        public int rentId { get; set; }
    }
}
