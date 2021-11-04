using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequest : Entity<MerchRequestId>
    {
        public MerchRequest(EmployeeId employeeId, MerchPack merchPack) =>
            (EmployeeId, MerchPack) = (employeeId, merchPack);
        
        public EmployeeId EmployeeId { get; }
        public MerchPack MerchPack { get; }
    }
}