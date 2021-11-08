using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class MerchRequestIdValueObjectTests
    {
        [Fact(DisplayName = "Значение сохраняется")]
        public void MerchRequestIdValues()
        {
            const ulong value = 3273284687623UL;
            Assert.Equal(value, new MerchRequestId(value).Value);
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new MerchRequestId(0UL), new MerchRequestId(0UL));
            Assert.Equal(new MerchRequestId(8888888888UL), new MerchRequestId(8888888888UL));
            Assert.NotEqual(new MerchRequestId(0UL), new MerchRequestId(364526423624UL));
            Assert.NotEqual(new MerchRequestId(0UL), new MerchRequestId(1UL));
        }
    }
}