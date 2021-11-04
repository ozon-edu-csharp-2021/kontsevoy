using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class MerchRequestId : ValueObject<ulong>
    {
        public MerchRequestId(ulong value) : base(value) {}
    }
}