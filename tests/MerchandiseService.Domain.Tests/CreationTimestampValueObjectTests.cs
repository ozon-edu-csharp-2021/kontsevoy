using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class CreationTimestampValueObjectTests
    {
        [Fact(DisplayName = "Значение сохраняется")]
        public void CreationTimestampValues()
        {
            var value = new DateTime(2021, 12, 31);
            Assert.Equal(value, new CreationTimestamp(value).Value);
        }
        
        [Fact(DisplayName = "Нельзя создать со датой до создания сервиса")]
        public void ConstructorDontAcceptOldValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreationTimestamp(new DateTime(2021, 11, 18)));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreationTimestamp(new DateTime(2020, 11, 30)));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreationTimestamp(new DateTime(2000, 11, 8)));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreationTimestamp(new DateTime(2021, 9, 1)));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreationTimestamp(new DateTime(2021, 5, 5)));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new CreationTimestamp(new DateTime(2021, 11, 20)), new CreationTimestamp(new DateTime(2021, 11, 20)));
            Assert.Equal(new CreationTimestamp(new DateTime(2022, 9, 1)), new CreationTimestamp(new DateTime(2022, 9, 1)));
            Assert.NotEqual(new CreationTimestamp(new DateTime(2022, 9, 1)), new CreationTimestamp(new DateTime(8888, 5, 5)));
            Assert.NotEqual(new CreationTimestamp(new DateTime(9999, 11, 8)), new CreationTimestamp(new DateTime(2027, 9, 1)));
        }
    }
}