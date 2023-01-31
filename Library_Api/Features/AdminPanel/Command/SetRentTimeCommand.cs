using MediatR;

namespace Library_Api.Features.AdminPanel.Command
{
    public class SetRentTimeCommand : IRequest
    {
        public int Days { get; set; }
    }
}