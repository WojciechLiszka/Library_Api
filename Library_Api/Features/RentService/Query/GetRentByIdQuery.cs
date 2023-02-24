using Library_Api.Entity;
using MediatR;

namespace Library_Api.Features.RentService.Query
{
    public class GetRentByIdQuery : IRequest<Rent>
    {
        public int rentId { get; set; }
    }
}