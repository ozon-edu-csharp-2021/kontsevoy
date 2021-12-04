using System;
using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class Name : ClassValueObject<string>
    {
        public Name(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameof(value)} of {nameof(Name)} must be non-empty string", nameof(value));
        }
        
        public static implicit operator Name(string value) => new(value);
    }
}