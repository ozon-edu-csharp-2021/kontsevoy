using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class EmployeeIdValueObjectTests
    {
        [Fact(DisplayName = "Значение сохраняется")]
        public void EmployeeIdValues()
        {
            const long value = 73284687623;
            Assert.Equal(value, new EmployeeId(value).Value);
        }
        
        [Fact(DisplayName = "Нельзя создать с нулевым значением")]
        public void CantBeCreateWithZeroValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new EmployeeId(0));
        }
        
        [Fact(DisplayName = "Нельзя создать с отрицательным значением")]
        public void ConstructorDontAcceptNegativeValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new EmployeeId(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EmployeeId(-9));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EmployeeId(-2035598237));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EmployeeId(int.MinValue));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new EmployeeId(1), new EmployeeId(1));
            Assert.Equal(new EmployeeId(982347983479), new EmployeeId(982347983479));
            Assert.NotEqual(new EmployeeId(1), new EmployeeId(982347983479));
            Assert.NotEqual(new EmployeeId(1), new EmployeeId(2));
        }
    }
}