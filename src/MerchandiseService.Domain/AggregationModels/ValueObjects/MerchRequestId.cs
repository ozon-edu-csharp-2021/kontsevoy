using System;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class MerchRequestId : ValueObject<long>
    {
        public MerchRequestId(long value) : base(value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} of {nameof(MerchRequestId)} must be greater than zero");
        }
    }
}