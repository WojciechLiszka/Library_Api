using Library_Api.Entity;
using Library_Api.Models;
using MediatR;

namespace Library_Api.Features.RentService.Query
{
    public class GetUserRentsQuery : IRequest<PagedResult<Rent>>
    {
        public int userId { get; set; }
        public RentQuery query { get; set; }
    }
}