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
            Assert.Throws<ArgumentNullException>(() => new MerchItem(MerchType.PenStarter, null));
        }
        
        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(
                new MerchItem(MerchType.SocksStarter, new PositiveQuantity(1)), 
                new MerchItem(MerchType.SocksStarter, new PositiveQuantity(1))
                );
            Assert.Equal(
                new MerchItem(MerchType.TShirtAfterProbation, new PositiveQuantity(2)), 
                new MerchItem(MerchType.TShirtAfterProbation, new PositiveQuantity(2))
                );     
            Assert.NotEqual(
                new MerchItem(MerchType.SweatshirtAfterProbation, new PositiveQuantity(5)), 
                new MerchItem(MerchType.TShirtStarter, new PositiveQuantity(5))
                );
            Assert.NotEqual(
                new MerchItem(MerchType.NotepadVeteran, new PositiveQuantity(8)), 
                new MerchItem(MerchType.NotepadVeteran, new PositiveQuantity(10))
                );
        }
        
        [Fact(DisplayName = "Можно получить переданный тип мерча")]
        public void CanObtainMerchType()
        {
            Assert.Equal(MerchType.SocksStarter, new MerchItem(MerchType.SocksStarter, new PositiveQuantity(1)).MerchType);
            Assert.Equal(MerchType.TShirtAfterProbation, new MerchItem(MerchType.TShirtAfterProbation, new PositiveQuantity(1)).MerchType);
        }
        
        [Fact(DisplayName = "Можно получить переданное количество")]
        public void CanObtainQuantity()
        {
            var quantity = new PositiveQuantity(45);
            
            Assert.Equal(quantity, new MerchItem(MerchType.SocksStarter, quantity).Quantity);
        }
    }
}