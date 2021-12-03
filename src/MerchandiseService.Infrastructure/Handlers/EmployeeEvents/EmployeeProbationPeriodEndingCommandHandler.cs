using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Infrastructure.Commands.EmployeeEvents;
using MerchandiseService.Infrastructure.Commands.MerchRequestAggregate;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Handlers.EmployeeEvents
{
    public class EmployeeProbationPeriodEndingCommandHandler : IRequestHandler<EmployeeProbationPeriodEndingCommand>
    {
        private ITracer Tracer { get; }
        private IMediator Mediator { get; }

        public EmployeeProbationPeriodEndingCommandHandler(ITracer tracer, IMediator mediator) => (Tracer, Mediator) = (tracer, mediator);
        
        public async Task<Unit> Handle(EmployeeProbationPeriodEndingCommand command, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(EmployeeProbationPeriodEndingCommandHandler)).StartActive();

            if (command.Payload?.MerchType != MerchPack.ProbationPeriodEnding.Id)
                throw new ArgumentException($"{nameof(command.Payload.MerchType)} don't match command {nameof(EmployeeProbationPeriodEndingCommand)}. Value {command.Payload?.MerchType}",
                    nameof(command));
            
            if (!command.Payload.ClothingSize.HasValue)
                throw new ArgumentException($"{nameof(command.Payload.ClothingSize)} must be provided",
                    nameof(command));

            await Mediator.Send(new CreateMerchRequestCommand
            {
                EmployeeEmail = command.Payload.EmployeeEmail,
                EmployeeName = command.Payload.EmployeeName,
                ManagerEmail = command.Payload.ManagerEmail,
                ManagerName = command.Payload.ManagerName,
                ClothingSize = command.Payload.ClothingSize.Value,
                MerchPackType = command.Payload.MerchType.Value
            }, cancellationToken);

            return Unit.Value;
        }
    }
}