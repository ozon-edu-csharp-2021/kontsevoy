using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class IdValueObjectTests
    {
        [Fact(DisplayName = "Значение сохраняется")]
        public void IdValues()
        {
            const long value = 3273284687623;
            Assert.Equal(value, new Id(value).Value);
        }
        
        [Fact(DisplayName = "Нельзя создать с нулевым значением")]
        public void CantBeCreateWithZeroValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Id(0));
        }
        
        [Fact(DisplayName = "Нельзя создать с отрицательным значением")]
        public void ConstructorDontAcceptNegativeValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Id(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Id(-9));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Id(-2035598237));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Id(int.MinValue));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new Id(1), new Id(1));
            Assert.Equal(new Id(8888888888), new Id(8888888888));
            Assert.NotEqual(new Id(1), new Id(364526423624));
            Assert.NotEqual(new Id(1), new Id(2));
        }
    }
}