using Library_Api.Entity;
using Library_Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.RentService.Query
{
    public class GetRentsQuery:IRequest<PagedResult<Rent>>
    {
        public RentQuery query { get; set; }
    }
}
