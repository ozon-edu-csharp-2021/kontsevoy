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
    public class EmployeeHiredCommandHandler : IRequestHandler<EmployeeHiredCommand>
    {
        private ITracer Tracer { get; }
        private IMediator Mediator { get; }

        public EmployeeHiredCommandHandler(ITracer tracer, IMediator mediator) => (Tracer, Mediator) = (tracer, mediator);
        
        public async Task<Unit> Handle(EmployeeHiredCommand command, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(EmployeeHiredCommandHandler)).StartActive();

            if (command.Payload?.MerchType != MerchPack.Welcome.Id)
                throw new ArgumentException($"{nameof(command.Payload.MerchType)} don't match command {nameof(EmployeeHiredCommand)}. Value {command.Payload?.MerchType}",
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