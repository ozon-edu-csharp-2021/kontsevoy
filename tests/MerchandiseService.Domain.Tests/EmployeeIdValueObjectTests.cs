using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class EmployeeIdValueObjectTests
    {
        [Fact(DisplayName = "Значение сохраняется")]
        public void EmployeeIdValues()
        {
            const ulong value = 73284687623UL;
            Assert.Equal(value, new EmployeeId(value).Value);
        }
    }
}