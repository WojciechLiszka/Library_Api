using MediatR;

namespace Library_Api.Features.AdminPanel.Command
{
    public class SetLateTimeFeeCommand : IRequest
    {
        public double fee { get; set; }
    }
}