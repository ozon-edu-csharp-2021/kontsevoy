using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class PositiveQuantityValueObjectTests
    {
        [Fact(DisplayName = "Можно создать с положительным значением")]
        public void CanBeCreateWithPositiveValues()
        {
            Assert.Equal(1, new PositiveQuantity(1).Value);
            Assert.Equal(89723498, new PositiveQuantity(89723498).Value);
            Assert.Equal(int.MaxValue, new PositiveQuantity(int.MaxValue).Value);
        }
        
        [Fact(DisplayName = "Нельзя создать с нулевым значением")]
        public void CantBeCreateWithZeroValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PositiveQuantity(0));
        }
        
        [Fact(DisplayName = "Нельзя создать с отрицательным значением")]
        public void ConstructorDontAcceptNegativeValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PositiveQuantity(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PositiveQuantity(-9));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PositiveQuantity(-2035598237));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PositiveQuantity(int.MinValue));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new PositiveQuantity(1), new PositiveQuantity(1));
            Assert.Equal(new PositiveQuantity(888), new PositiveQuantity(888));
            Assert.NotEqual(new PositiveQuantity(2), new PositiveQuantity(3645264));
            Assert.NotEqual(new PositiveQuantity(2), new PositiveQuantity(1));
        }
    }
}