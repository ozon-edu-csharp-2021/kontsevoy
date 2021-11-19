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
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(null, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(null, null, null, null, MerchPack.Veteran));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(null, new Id(1), null, null, null));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, null, new Email("e@mail.ru"), ClothingSize.L, MerchPack.Welcome));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, new Id(1), null, ClothingSize.L, MerchPack.Welcome));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, new Id(1), new Email("e@mail.ru"), null, MerchPack.Welcome));
            Assert.Throws<ArgumentNullException>(() => MerchRequest.New(CreationTimestamp.Now, new Id(1), new Email("e@mail.ru"), ClothingSize.XL, null));
        }
        
        // [Fact(DisplayName = "Можно получить переданный идентификатор сотрудника")]
        // public void CanObtainEmployeeId()
        // {
        //     var id = new Id(821479);
        //     Assert.Equal(id, new MerchRequest(id, MerchPack.Welcome).EmployeeId);
        // }
        //
        // [Fact(DisplayName = "Можно получить переданный MerchPack")]
        // public void CanObtainMerchPack()
        // {
        //     var merchRequest = new MerchRequest(new Id(1), MerchPack.ConferenceListener);
        //     
        //     Assert.Equal(MerchPack.ConferenceListener, merchRequest.MerchPack);
        // }
        //
        // [Fact(DisplayName = "Можно задать идентификатор")]
        // public void CanSetMerchRequestId()
        // {
        //     var id = new Id(821479);
        //     var merchRequest = new MerchRequest(new Id(1), MerchPack.Welcome)
        //     {
        //         Id = id
        //     };
        //     
        //     Assert.Equal(id, merchRequest.Id);
        // }
        //
        // [Fact(DisplayName = "Нельзя менять идентификатор")]
        // public void CantChangeMerchRequestId()
        // {
        //     var id = new Id(8);
        //     var merchRequest = new MerchRequest(new Id(1), MerchPack.Welcome)
        //     {
        //         Id = id
        //     };
        //     var otherId = new Id(982179128);
        //     
        //     Assert.Throws<InvalidOperationException>(() => merchRequest.Id = otherId);
        // }
    }
}