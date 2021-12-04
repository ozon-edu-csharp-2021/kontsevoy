using System.Collections.Generic;
using System.Collections.Immutable;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Infrastructure.Queries.StockApi
{
    public class StockApiItemsQuery : IRequest<StockApiItemsQueryResponse>
    {
        public StockApiItemsQuery(bool forceRequest = false) => ForceRequest = forceRequest; 
        public bool ForceRequest { get; } 
    }

    public class StockApiItemsQueryResponse
    {
        public ImmutableDictionary<long, MerchItem> Dictionary { get; init; }
        
        public ImmutableDictionary<MerchType, ImmutableDictionary<ClothingSize, long>> Sized { get; init; }
        public ImmutableDictionary<MerchType, long> Unsized { get; init; }
    }

    public class MerchItem : ValueObject
    {
        public long Sku { get; init; }
        public MerchType Type { get; init; }
        public string TypeName { get; init; }
        public ImmutableHashSet<MerchPack> Packs { get; init; }
        public ClothingSize ClothingSize { get; init; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Sku;
            yield return Type;
            yield return ClothingSize;
        }
    }
}