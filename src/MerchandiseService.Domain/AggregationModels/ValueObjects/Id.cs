using System;
using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class Id : ValueObject<long>
    {
        public Id(long value) : base(value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} of {nameof(Id)} must be greater than zero");
        }

        public static implicit operator Id(long value) => new(value);
    }
}