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
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new EmployeeId(0UL), new EmployeeId(0UL));
            Assert.Equal(new EmployeeId(982347983479UL), new EmployeeId(982347983479UL));
            Assert.NotEqual(new EmployeeId(0UL), new EmployeeId(982347983479UL));
            Assert.NotEqual(new EmployeeId(0UL), new EmployeeId(1UL));
        }
    }
}