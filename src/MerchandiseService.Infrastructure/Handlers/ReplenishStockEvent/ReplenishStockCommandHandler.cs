using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Infrastructure.Commands.ReplenishStockEvent;
using MerchandiseService.Infrastructure.Queries.StockApi;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Handlers.ReplenishStockEvent
{
    public class ReplenishStockCommandHandler : IRequestHandler<ReplenishStockCommand>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }
        private IMediator Mediator { get; }
        private ILogger<ReplenishStockCommandHandler> Logger { get; }
        private ITracer Tracer { get; }

        public ReplenishStockCommandHandler(IMerchRequestRepository merchRequestRepository, ITracer tracer,
            IMediator mediator, ILogger<ReplenishStockCommandHandler> logger) =>
            (MerchRequestRepository, Tracer, Mediator, Logger) = (merchRequestRepository, tracer, mediator, logger);
        
        public async Task<Unit> Handle(ReplenishStockCommand command, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(ReplenishStockCommandHandler)).StartActive();

            var allSkus = await Mediator.Send(new StockApiItemsQuery(), cancellationToken);
            var skus = command.Items.Select(f => f.Sku).ToHashSet();
            var packs = new HashSet<MerchPack>();
            foreach (var sku in skus)
                if (allSkus.Dictionary.TryGetValue(sku, out var merchItem))
                    packs.UnionWith(merchItem.Packs);
            
            var requests = await MerchRequestRepository.FindByStatus(MerchRequestStatus.Awaiting, cancellationToken);
            foreach (var request in requests)
            {
                if (!packs.Contains(request.MerchPack)) continue;
                
                try
                {
                    request.ReadyToProcessing();
                    await MerchRequestRepository.UpdateAsync(request, cancellationToken);
                }
                catch (Exception e)
                {
                    Logger.LogError("Error while change request status. Message {message}", e.Message);
                }
            }

            return Unit.Value;
        }
    }
}