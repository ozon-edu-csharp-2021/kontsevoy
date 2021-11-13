using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Models;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class MerchKindEnumerationTests
    {
        [Fact(DisplayName = "Отсутствие экземпляра с кодом 0")]
        public void CantGetValueByZeroId()
        {
            Assert.Throws<ArgumentException>(() => MerchKind.GetById(0));
        }
        
        [Fact(DisplayName = "Равенство экземпляров по ссылкам")]
        public void ReferenceEquality()
        {
            Assert.Same(MerchKind.Accessory, MerchKind.GetById(MerchKind.Accessory.Id));
            Assert.Same(MerchKind.Clothing, MerchKind.GetById(MerchKind.Clothing.Id));
        }
        
        [Fact(DisplayName = "Все предопределённые значения можно получить через GetById")]
        public void GetByIdWorks()
        {
            foreach (var value in Enumeration.GetAll<MerchKind>())
                Assert.Same(value, MerchKind.GetById(value.Id));
        }
    }
}