using System;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class EmployeeEntityTests
    {
        [Fact(DisplayName = "Нельзя создать с null аргументами")]
        public void ConstructorDontAcceptNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new Employee(null, null, null));
            Assert.Throws<ArgumentNullException>(() => new Employee(null, new Email("some@email.ru"), ClothingSize.XXL));
            Assert.Throws<ArgumentNullException>(() => new Employee(new EmployeeId(0), null, ClothingSize.XL));
            Assert.Throws<ArgumentNullException>(() => new Employee(new EmployeeId(23874523456), new Email("some@other.email"), null));
            Assert.Throws<ArgumentNullException>(() => new Employee(null, new Email("some@other-good.email"), null));
            Assert.Throws<ArgumentNullException>(() => new Employee(null, null, ClothingSize.XL));
            Assert.Throws<ArgumentNullException>(() => new Employee(new EmployeeId(87427029852740), null, null));
        }
        
        [Fact(DisplayName = "Нельзя занулить размер одежды")]
        public void ChangeClothingSizeDontAcceptNullArgument()
        {
            var employee = new Employee(new EmployeeId(0), new Email("some@good.email"), ClothingSize.XL);
            
            Assert.Throws<ArgumentNullException>(() => employee.ChangeClothingSize(null));
        }
        
        [Fact(DisplayName = "Нельзя занулить адрес электронной почты")]
        public void ChangeNotificationEmailDontAcceptNullArgument()
        {
            var employee = new Employee(new EmployeeId(2037093), new Email("some@email.ru"), ClothingSize.XXL);
            
            Assert.Throws<ArgumentNullException>(() => employee.ChangeNotificationEmail(null));
        }
        
        [Fact(DisplayName = "Можно поменять размер одежды")]
        public void CanChangeClothingSize()
        {
            var employee = new Employee(new EmployeeId(2037093), new Email("some@email.ru"), ClothingSize.XL);
            employee.ChangeClothingSize(ClothingSize.XXL);
            Assert.Equal(ClothingSize.XXL, employee.ClothingSize);
            
            employee.ChangeClothingSize(ClothingSize.XS);
            Assert.Equal(ClothingSize.XS, employee.ClothingSize);
        }
        
        [Fact(DisplayName = "Можно поменять адрес электронной почты")]
        public void CanChangeNotificationEmail()
        {
            var email = new Email("good@email.ru");
            var employee = new Employee(new EmployeeId(203557093), email, ClothingSize.S);
            Assert.Equal(email, employee.NotificationEmail);
            
            var otherEmail = new Email("other@email.ru");
            employee.ChangeNotificationEmail(otherEmail);
            Assert.Equal(otherEmail, employee.NotificationEmail);
        }
        
        [Fact(DisplayName = "Можно получить переданный идентификатор")]
        public void CanObtainEmployeeId()
        {
            var id = new EmployeeId(821479);
            Assert.Equal(id, new Employee(id, new Email("good@email.com"), ClothingSize.XXL).Id);
        }
    }
}