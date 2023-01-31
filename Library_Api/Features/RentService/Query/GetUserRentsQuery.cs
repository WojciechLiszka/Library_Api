using Library_Api.Entity;
using MediatR;

namespace Library_Api.Features.RentService.Query
{
    public class GetUserRentsQuery : IRequest<List<Rent>>
    {
        public int userId { get; set; }
    }
}