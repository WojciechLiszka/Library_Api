using MediatR;

namespace Library_Api.Features.AdminPanel.Command
{
    public class SetRentTimeCommandHandler : IRequestHandler<SetRentTimeCommand, Unit>
    {
        private readonly Configuration _configuration = Configuration.GetInstance();

        public SetRentTimeCommandHandler()
        {
        }

        public Task<Unit> Handle(SetRentTimeCommand request, CancellationToken cancellationToken)
        {
            _configuration.RentDays = request.Days;
            return Task.FromResult(Unit.Value);
        }
    }
}