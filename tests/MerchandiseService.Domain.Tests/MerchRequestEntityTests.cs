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
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(null, null));
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(null, MerchPack.Veteran));
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(new EmployeeId(0UL), null));
        }
        
        [Fact(DisplayName = "Можно получить переданный идентификатор сотрудника")]
        public void CanObtainEmployeeId()
        {
            var id = new EmployeeId(821479UL);
            Assert.Equal(id, new MerchRequest(id, MerchPack.Welcome).EmployeeId);
        }
        
        [Fact(DisplayName = "Можно получить переданный MerchPack")]
        public void CanObtainMerchPack()
        {
            var merchRequest = new MerchRequest(new EmployeeId(0UL), MerchPack.ConferenceListener);
            
            Assert.Equal(MerchPack.ConferenceListener, merchRequest.MerchPack);
        }
        
        [Fact(DisplayName = "Можно задать идентификатор")]
        public void CanSetMerchRequestId()
        {
            var id = new MerchRequestId(821479UL);
            var merchRequest = new MerchRequest(new EmployeeId(0UL), MerchPack.Welcome)
            {
                Id = id
            };
            
            Assert.Equal(id, merchRequest.Id);
        }
        
        [Fact(DisplayName = "Нельзя менять идентификатор")]
        public void CantChangeMerchRequestId()
        {
            var id = new MerchRequestId(8UL);
            var merchRequest = new MerchRequest(new EmployeeId(0UL), MerchPack.Welcome)
            {
                Id = id
            };
            var otherId = new MerchRequestId(982179128);
            
            Assert.Throws<InvalidOperationException>(() => merchRequest.Id = otherId);
        }
    }
}