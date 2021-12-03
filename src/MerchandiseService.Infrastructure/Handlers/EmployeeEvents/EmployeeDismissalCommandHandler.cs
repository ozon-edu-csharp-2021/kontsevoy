using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Infrastructure.Commands.EmployeeEvents;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Handlers.EmployeeEvents
{
    public class EmployeeDismissalCommandHandler : IRequestHandler<EmployeeDismissalCommand>
    {
        private ITracer Tracer { get; }
        private IMediator Mediator { get; }

        public EmployeeDismissalCommandHandler(ITracer tracer, IMediator mediator) => (Tracer, Mediator) = (tracer, mediator);
        
        public async Task<Unit> Handle(EmployeeDismissalCommand command, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(EmployeeDismissalCommandHandler)).StartActive();

            await Task.Delay(1, cancellationToken);

            return Unit.Value;
        }
    }
}