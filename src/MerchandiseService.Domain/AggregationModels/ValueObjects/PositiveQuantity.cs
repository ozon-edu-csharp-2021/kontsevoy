using System;
using MerchandiseService.Domain.Base.Models;

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
        
        public static implicit operator PositiveQuantity(int value) => new(value);
    }
}