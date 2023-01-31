using MediatR;

namespace Library_Api.Features.AdminPanel.Command
{
    public class SetLateTimeFeeCommandHandler : IRequestHandler<SetLateTimeFeeCommand, Unit>
    {
        private readonly Configuration _configuration = Configuration.GetInstance();

        public Task<Unit> Handle(SetLateTimeFeeCommand request, CancellationToken cancellationToken)
        {
            _configuration.Latefee = request.fee;
            return Task.FromResult(Unit.Value);
        }
    }
}