using System;
using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class NonNegativeQuantity : ValueObject<int>
    {
        public NonNegativeQuantity(int value) : base(value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} of {nameof(NonNegativeQuantity)} must not be less than zero");
        }
        
        public static implicit operator NonNegativeQuantity(int value) => new(value);
    }
}