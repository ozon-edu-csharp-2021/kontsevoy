using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class MerchRequestIdValueObjectTests
    {
        [Fact(DisplayName = "Значение сохраняется")]
        public void MerchRequestIdValues()
        {
            const long value = 3273284687623;
            Assert.Equal(value, new MerchRequestId(value).Value);
        }
        
        [Fact(DisplayName = "Нельзя создать с нулевым значением")]
        public void CantBeCreateWithZeroValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MerchRequestId(0));
        }
        
        [Fact(DisplayName = "Нельзя создать с отрицательным значением")]
        public void ConstructorDontAcceptNegativeValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MerchRequestId(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new MerchRequestId(-9));
            Assert.Throws<ArgumentOutOfRangeException>(() => new MerchRequestId(-2035598237));
            Assert.Throws<ArgumentOutOfRangeException>(() => new MerchRequestId(int.MinValue));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new MerchRequestId(1), new MerchRequestId(1));
            Assert.Equal(new MerchRequestId(8888888888), new MerchRequestId(8888888888));
            Assert.NotEqual(new MerchRequestId(1), new MerchRequestId(364526423624));
            Assert.NotEqual(new MerchRequestId(1), new MerchRequestId(2));
        }
    }
}