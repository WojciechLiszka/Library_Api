﻿using MediatR;

namespace Library_Api.Features.AdminPanel.Command
{
    public class SetLateTimeFeeCommandHandler : IRequestHandler<SetLateTimeFeeCommand, Unit>
    {
        private readonly ApiConfiguration _configuration = ApiConfiguration.GetInstance();

        public Task<Unit> Handle(SetLateTimeFeeCommand request, CancellationToken cancellationToken)
        {
            _configuration.Latefee = request.Fee;
            return Task.FromResult(Unit.Value);
        }
    }
}