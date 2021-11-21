using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class HandoutTimestampValueObjectTests
    {
        [Fact(DisplayName = "Значение сохраняется")]
        public void HandoutTimestampValues()
        {
            var value = new DateTime(2021, 12, 31);
            Assert.Equal(value, new HandoutTimestamp(value).Value);
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new HandoutTimestamp(new DateTime(2021, 11, 20)), new HandoutTimestamp(new DateTime(2021, 11, 20)));
            Assert.Equal(new HandoutTimestamp(new DateTime(2022, 9, 1)), new HandoutTimestamp(new DateTime(2022, 9, 1)));
            Assert.NotEqual(new HandoutTimestamp(new DateTime(2022, 9, 1)), new HandoutTimestamp(new DateTime(8888, 5, 5)));
            Assert.NotEqual(new HandoutTimestamp(new DateTime(9999, 11, 8)), new HandoutTimestamp(new DateTime(2027, 9, 1)));
        }
    }
}