using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Models;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class MerchPackEnumerationTests
    {
        [Fact(DisplayName = "Отсутствие экземпляра с кодом 0")]
        public void CantGetValueByZeroId()
        {
            Assert.Throws<ArgumentException>(() => MerchPack.GetById(0));
        }
        
        [Fact(DisplayName = "Равенство экземпляров по ссылкам")]
        public void ReferenceEquality()
        {
            Assert.Same(MerchPack.Welcome, MerchPack.GetById(MerchPack.Welcome.Id));
            Assert.Same(MerchPack.ProbationPeriodEnding, MerchPack.GetById(MerchPack.ProbationPeriodEnding.Id));
            Assert.Same(MerchPack.ConferenceListener, MerchPack.GetById(MerchPack.ConferenceListener.Id));
            Assert.Same(MerchPack.ConferenceSpeaker, MerchPack.GetById(MerchPack.ConferenceSpeaker.Id));
            Assert.Same(MerchPack.Veteran, MerchPack.GetById(MerchPack.Veteran.Id));
        }
        
        [Fact(DisplayName = "Все предопределённые значения можно получить через GetById")]
        public void GetByIdWorks()
        {
            foreach (var value in Enumeration.GetAll<MerchPack>())
                Assert.Same(value, MerchPack.GetById(value.Id));
        }
        
        [Fact(DisplayName = "Все предопределённые значения можно получить через GetByName")]
        public void GetByNameWorks()
        {
            foreach (var value in Enumeration.GetAll<MerchPack>())
                Assert.Same(value, MerchPack.GetByName(value.Name));
        }
    }
}