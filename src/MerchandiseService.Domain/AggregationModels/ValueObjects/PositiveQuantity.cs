using System;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class PositiveQuantity : ValueObject<int>
    {
        public PositiveQuantity(int value) : base(value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} of {nameof(PositiveQuantity)} must be greater than zero");
        }
    }
}