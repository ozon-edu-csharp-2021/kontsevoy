using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Infrastructure.Commands.ReplenishStock;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class ReplenishStockCommandHandler : IRequestHandler<ReplenishStockCommand>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }

        public ReplenishStockCommandHandler(IMerchRequestRepository merchRequestRepository)
            => MerchRequestRepository = merchRequestRepository;
        
        public async Task<Unit> Handle(ReplenishStockCommand request, CancellationToken cancellationToken)
        {
            var requests = await MerchRequestRepository.FindByStatus(MerchRequestStatus.Awaiting, cancellationToken);

            return Unit.Value;
        }
    }
}