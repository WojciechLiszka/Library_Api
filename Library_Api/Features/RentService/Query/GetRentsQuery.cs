using Library_Api.Entity;
using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.RentService.Query
{
    public class GetRentsQuery : IRequest<PagedResult<Rent>>
    {
        public RentQuery query { get; set; }
    }
}