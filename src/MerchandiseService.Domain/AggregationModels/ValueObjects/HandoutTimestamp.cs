using System;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class HandoutTimestamp : ValueObject<DateTime>
    {
        public HandoutTimestamp(DateTime value) : base(value) {}
        
        public static implicit operator HandoutTimestamp(DateTime value) => new(value);
        public static implicit operator HandoutTimestamp(DateTime? value)
            => value is not null ? new HandoutTimestamp(value.Value) : default;

        public static implicit operator DateTime?(HandoutTimestamp value) => value?.Value;

        public static CreationTimestamp Now => new(DateTime.Now);
    }
}