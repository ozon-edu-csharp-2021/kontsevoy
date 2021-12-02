using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Infrastructure.Commands.ReplenishStock;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class ReplenishStockCommandHandler : IRequestHandler<ReplenishStockCommand>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }
        private ITracer Tracer { get; }

        public ReplenishStockCommandHandler(IMerchRequestRepository merchRequestRepository, ITracer tracer) =>
            (MerchRequestRepository, Tracer) = (merchRequestRepository, tracer);
        
        public async Task<Unit> Handle(ReplenishStockCommand request, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(ReplenishStockCommandHandler)).StartActive();
            
            var requests = await MerchRequestRepository.FindByStatus(MerchRequestStatus.Awaiting, cancellationToken);

            return Unit.Value;
        }
    }
}