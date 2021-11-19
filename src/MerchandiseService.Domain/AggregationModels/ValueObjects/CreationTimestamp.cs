using System;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class CreationTimestamp : ValueObject<DateTime>
    {
        public CreationTimestamp(DateTime value) : base(value)
        {
            var theDay = new DateTime(2021, 11, 19);
            if (value <= theDay)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} [{value}] of {nameof(CreationTimestamp)} must be greater than {theDay}");
        }

        public static implicit operator CreationTimestamp(DateTime value) => new(value);

        public static CreationTimestamp Now => new(DateTime.Now);
    }
}