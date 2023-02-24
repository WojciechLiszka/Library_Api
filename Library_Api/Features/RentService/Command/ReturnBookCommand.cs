using MediatR;

namespace Library_Api.Features.RentService.Command
{
    public class ReturnBookCommand : IRequest<double>
    {
        public int RentId { get; set; }
    }
}