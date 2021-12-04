using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Base.Models;
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
            Assert.Same(MerchType.SocksStarter, MerchType.GetById(MerchType.SocksStarter.Id));
            Assert.Same(MerchType.PenStarter, MerchType.GetById(MerchType.PenStarter.Id));
            Assert.Same(MerchType.TShirtAfterProbation, MerchType.GetById(MerchType.TShirtAfterProbation.Id));
            Assert.Same(MerchType.SweatshirtAfterProbation, MerchType.GetById(MerchType.SweatshirtAfterProbation.Id));
            Assert.Same(MerchType.SweatshirtVeteran, MerchType.GetById(MerchType.SweatshirtVeteran.Id));
            Assert.Same(MerchType.TShirtStarter, MerchType.GetById(MerchType.TShirtStarter.Id));
        }
        
        [Fact(DisplayName = "Все предопределённые значения можно получить через GetById")]
        public void GetByIdWorks()
        {
            foreach (var value in Enumeration.GetAll<MerchType>())
                Assert.Same(value, MerchType.GetById(value.Id));
        }
        
        [Fact(DisplayName = "Все предопределённые значения можно получить через GetByName")]
        public void GetByNameWorks()
        {
            foreach (var value in Enumeration.GetAll<MerchType>())
                Assert.Same(value, MerchType.GetByName(value.Name));
        }
    }
}