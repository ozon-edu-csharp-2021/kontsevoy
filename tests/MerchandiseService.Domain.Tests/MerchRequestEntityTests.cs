using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class MerchRequestEntityTests
    {
        [Fact(DisplayName = "Нельзя создать с null аргументами")]
        public void ConstructorDontAcceptNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(null, null, null, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(null, null, null, null, null, null, MerchPack.Veteran));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(null, "w@e.ru", null, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, null, null, new Email("e@mail.ru"), null, ClothingSize.L, MerchPack.Welcome));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, "w@e.ru", null, null, null, ClothingSize.L, MerchPack.Welcome));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, "w@e.ru", null, new Email("e@mail.ru"), null, null, MerchPack.Welcome));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, "w@e.ru", null, new Email("e@mail.ru"), null, ClothingSize.XL, null));
        }
        
        [Fact(DisplayName = "Можно получить заданный MerchPack")]
        public void CanObtainMerchPack()
        {
            var merchRequest = MerchRequest.New(
                CreationTimestamp.Now, 
                "emloyee@ozon.ru", "employee",
                "manager@ozon.ru", "manager",
                ClothingSize.L,
                MerchPack.Veteran);
            
            Assert.Equal(MerchPack.Veteran, merchRequest.MerchPack);
        }
        
        [Fact(DisplayName = "Можно задать идентификатор")]
        public void CanSetMerchRequestId()
        {
            var id = new Id(821479);
            var merchRequest = MerchRequest.New(
                CreationTimestamp.Now, 
                "emloyee@ozon.ru", "employee",
                "manager@ozon.ru", "manager",
                ClothingSize.L,
                MerchPack.Veteran);
            merchRequest.Id = id;
            
            Assert.Equal(id, merchRequest.Id);
        }
        
        [Fact(DisplayName = "Нельзя менять идентификатор")]
        public void CantChangeMerchRequestId()
        {
            var id = new Id(8);
            var merchRequest = MerchRequest.New(
                CreationTimestamp.Now, 
                "emloyee@ozon.ru", "employee",
                "manager@ozon.ru", "manager",
                ClothingSize.L,
                MerchPack.Veteran);
            merchRequest.Id = id;
            
            var otherId = new Id(982179128);
            
            Assert.Throws<InvalidOperationException>(() => merchRequest.Id = otherId);
        }
    }
}