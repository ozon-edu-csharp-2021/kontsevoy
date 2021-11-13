using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Models;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class MerchTypeEnumerationTests
    {
        [Fact(DisplayName = "Отсутствие экземпляра с кодом 0")]
        public void CantGetValueByZeroId()
        {
            Assert.Throws<ArgumentException>(() => MerchType.GetById(0));
        }
        
        [Fact(DisplayName = "Равенство экземпляров по ссылкам")]
        public void ReferenceEquality()
        {
            Assert.Same(MerchType.Bag, MerchType.GetById(MerchType.Bag.Id));
            Assert.Same(MerchType.Notepad, MerchType.GetById(MerchType.Notepad.Id));
            Assert.Same(MerchType.Pen, MerchType.GetById(MerchType.Pen.Id));
            Assert.Same(MerchType.Socks, MerchType.GetById(MerchType.Socks.Id));
            Assert.Same(MerchType.Sweatshirt, MerchType.GetById(MerchType.Sweatshirt.Id));
            Assert.Same(MerchType.TShirt, MerchType.GetById(MerchType.TShirt.Id));
        }
        
        [Fact(DisplayName = "Все предопределённые значения можно получить через GetById")]
        public void GetByIdWorks()
        {
            foreach (var value in Enumeration.GetAll<MerchType>())
                Assert.Same(value, MerchType.GetById(value.Id));
        }
    }
}