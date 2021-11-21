using System;
using System.Text.Json;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class Handout : ClassValueObject<string>
    {
        public Handout(string value) : base(value)
        {
            try
            {
                JsonDocument.Parse(value);
            }
            catch (JsonException e)
            {
                throw new ArgumentException($"{nameof(value)} must be valid json", nameof(value), e);
            }
        }

        public static implicit operator Handout(string value) => value is null ? default : new(value);
    }
}