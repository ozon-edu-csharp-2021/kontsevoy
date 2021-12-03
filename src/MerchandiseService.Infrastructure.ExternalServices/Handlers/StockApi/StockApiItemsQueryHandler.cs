using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Base.Models;
using MerchandiseService.Infrastructure.Queries.StockApi;
using Microsoft.Extensions.Caching.Memory;
using OpenTracing;
using OzonEdu.StockApi.Grpc;

namespace MerchandiseService.Infrastructure.ExternalServices.Handlers.StockApi
{
    public class StockApiItemsQueryHandler : IRequestHandler<StockApiItemsQuery, StockApiItemsQueryResponse>
    {
        private IMemoryCache Cache { get; }
        private ITracer Tracer { get; }
        private StockApiGrpc.StockApiGrpcClient Client { get; }
        private static string Key => "skus_dictionary";

        public StockApiItemsQueryHandler(IMemoryCache memoryCache, ITracer tracer, StockApiGrpc.StockApiGrpcClient client) =>
            (Cache, Tracer, Client) = (memoryCache, tracer, client);
        
        public async Task<StockApiItemsQueryResponse> Handle(StockApiItemsQuery command, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(StockApiItemsQueryHandler)).StartActive();

            if (!command.ForceRequest && Cache.TryGetValue(Key, out StockApiItemsQueryResponse result)) return result;

            var response = await Client.GetAllStockItemsAsync(new Empty(), cancellationToken: cancellationToken);

            var typeDictionary = new Dictionary<MerchType, HashSet<MerchPack>>();

            foreach (var merchPack in Enumeration.GetAll<MerchPack>())
                foreach (var merchItem in merchPack.Items)
                    typeDictionary.GetValueOrDefault(merchItem.MerchType, new HashSet<MerchPack>()).Add(merchPack);

            var dictionary = new Dictionary<long, MerchItem>();
            var sized = new Dictionary<MerchType, Dictionary<ClothingSize, long>>();
            var unsized = new Dictionary<MerchType, long>();

            foreach (var item in response.Items)
            {
                var type = (MerchType)item.ItemTypeId;
                var clothingSize = (ClothingSize)item.SizeId;
                var merchItem = new MerchItem
                {
                    Sku = item.Sku,
                    ClothingSize = clothingSize,
                    Type = type,
                    TypeName = item.ItemName,
                    Packs = typeDictionary[(MerchType)item.ItemTypeId].ToImmutableHashSet()
                };
                dictionary.Add(item.Sku, merchItem);
                if (clothingSize is not null)
                    sized.GetValueOrDefault(type, new Dictionary<ClothingSize, long>())[clothingSize] = item.Sku;
                else
                    unsized[type] = item.Sku;
            }

            result = new StockApiItemsQueryResponse
            {
                Dictionary = dictionary.ToImmutableDictionary(),
                Sized = sized.ToImmutableDictionary(pair => pair.Key, pair => pair.Value.ToImmutableDictionary()),
                Unsized = unsized.ToImmutableDictionary()
            };
            Cache.Set(Key, result);
            return result;
        }
    }
}