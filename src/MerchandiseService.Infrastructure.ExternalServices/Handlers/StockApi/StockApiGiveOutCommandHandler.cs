using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Base.Models;
using MerchandiseService.Infrastructure.Commands.StockApi;
using MerchandiseService.Infrastructure.Queries.StockApi;
using Microsoft.Extensions.Caching.Memory;
using OpenTracing;
using OzonEdu.StockApi.Grpc;

namespace MerchandiseService.Infrastructure.ExternalServices.Handlers.StockApi
{
    public class StockApiGiveOutCommandHandler : IRequestHandler<StockApiGiveOutCommand, bool>
    {
        private ITracer Tracer { get; }
        private StockApiGrpc.StockApiGrpcClient Client { get; }
        private static string Key => "skus_dictionary";

        public StockApiGiveOutCommandHandler(ITracer tracer, StockApiGrpc.StockApiGrpcClient client) =>
            (Tracer, Client) = (tracer, client);
        
        public async Task<bool> Handle(StockApiGiveOutCommand command, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(StockApiGiveOutCommandHandler)).StartActive();

            var request = new GiveOutItemsRequest();
            request.Items.Add(command.Items.Select(f => new SkuQuantityItem {Sku = f.Sku, Quantity = f.Quantity}));
            
            var response = await Client.GiveOutItemsAsync(request, cancellationToken: cancellationToken);
            return (response.Result == GiveOutItemsResponse.Types.Result.Successful);
        }
    }
}