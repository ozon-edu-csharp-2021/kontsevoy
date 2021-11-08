using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class NonNegativeQuantityValueObjectTests
    {
        [Fact(DisplayName = "Можно создать с положительным значением")]
        public void CanBeCreateWithPositiveValues()
        {
            Assert.Equal(1, new NonNegativeQuantity(1).Value);
            Assert.Equal(89723498, new NonNegativeQuantity(89723498).Value);
        }
        
        [Fact(DisplayName = "Можно создать с нулевым значением")]
        public void CanBeCreateWithZeroValue()
        {
            Assert.Equal(0, new NonNegativeQuantity(0).Value);
        }
        
        [Fact(DisplayName = "Нельзя создать с отрицательным значением")]
        public void ConstructorDontAcceptNegativeValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonNegativeQuantity(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonNegativeQuantity(-8));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonNegativeQuantity(-2035798237));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonNegativeQuantity(int.MinValue));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new NonNegativeQuantity(0), new NonNegativeQuantity(0));
            Assert.Equal(new NonNegativeQuantity(888), new NonNegativeQuantity(888));
            Assert.NotEqual(new NonNegativeQuantity(0), new NonNegativeQuantity(3645264));
            Assert.NotEqual(new NonNegativeQuantity(0), new NonNegativeQuantity(1));
        }
    }
}