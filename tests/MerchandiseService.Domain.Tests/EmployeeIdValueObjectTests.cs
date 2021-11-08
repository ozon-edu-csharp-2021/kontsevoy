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
            Assert.Equal(new EmployeeId(0), new EmployeeId(0));
            Assert.Equal(new EmployeeId(982347983479), new EmployeeId(982347983479));
            Assert.NotEqual(new EmployeeId(0), new EmployeeId(982347983479));
            Assert.NotEqual(new EmployeeId(0), new EmployeeId(1));
        }
    }
}