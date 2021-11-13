using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class MerchItemValueObjectTests
    {
        [Fact(DisplayName = "Нельзя создать с null аргументами")]
        public void ConstructorDontAcceptNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new MerchItem(null, null));
            Assert.Throws<ArgumentNullException>(() => new MerchItem(null, new PositiveQuantity(1)));
            Assert.Throws<ArgumentNullException>(() => new MerchItem(MerchType.Notepad, null));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(
                new MerchItem(MerchType.Bag, new PositiveQuantity(1)), 
                new MerchItem(MerchType.Bag, new PositiveQuantity(1))
                );
            Assert.Equal(
                new MerchItem(MerchType.Pen, new PositiveQuantity(2)), 
                new MerchItem(MerchType.Pen, new PositiveQuantity(2))
                );     
            Assert.NotEqual(
                new MerchItem(MerchType.Socks, new PositiveQuantity(5)), 
                new MerchItem(MerchType.TShirt, new PositiveQuantity(5))
                );
            Assert.NotEqual(
                new MerchItem(MerchType.Sweatshirt, new PositiveQuantity(8)), 
                new MerchItem(MerchType.Sweatshirt, new PositiveQuantity(10))
                );
        }
        
        [Fact(DisplayName = "Можно получить переданный тип мерча")]
        public void CanObtainMerchType()
        {
            Assert.Equal(MerchType.Bag, new MerchItem(MerchType.Bag, new PositiveQuantity(1)).MerchType);
            Assert.Equal(MerchType.Pen, new MerchItem(MerchType.Pen, new PositiveQuantity(1)).MerchType);
        }
        
        [Fact(DisplayName = "Можно получить переданное количество")]
        public void CanObtainQuantity()
        {
            var quantity = new PositiveQuantity(45);
            
            Assert.Equal(quantity, new MerchItem(MerchType.Bag, quantity).Quantity);
        }
    }
}