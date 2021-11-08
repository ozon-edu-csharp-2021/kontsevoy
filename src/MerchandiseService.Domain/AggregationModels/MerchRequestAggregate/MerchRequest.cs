using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequest : Entity<MerchRequestId>
    {
        public MerchRequest(EmployeeId employeeId, MerchPack merchPack)
        {
            EmployeeId = employeeId ?? throw new ArgumentNullException(nameof(employeeId), 
                $"{nameof(employeeId)} must be provided");
            MerchPack = merchPack ?? throw new ArgumentNullException(nameof(merchPack), 
                $"{nameof(merchPack)} must be provided");
        }
        
        public EmployeeId EmployeeId { get; }
        public MerchPack MerchPack { get; }
    }
}