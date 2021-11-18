using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequest : Entity<Id>
    {
        public MerchRequest(
            Id employeeId,
            Email employeeNotificationEmail,
            ClothingSize employeeClothingSize,
            MerchPack merchPack,
            MerchRequestStatus status)
        {
            EmployeeId = employeeId 
                         ?? throw new ArgumentNullException(nameof(employeeId),
                             $"{nameof(employeeId)} must be provided");
            EmployeeNotificationEmail = employeeNotificationEmail 
                                        ?? throw new ArgumentNullException(nameof(employeeNotificationEmail),
                                            $"{nameof(employeeNotificationEmail)} must be provided");
            EmployeeClothingSize = employeeClothingSize
                                   ?? throw new ArgumentNullException(nameof(employeeClothingSize),
                                       $"{nameof(employeeClothingSize)} must be provided");
            MerchPack = merchPack
                        ?? throw new ArgumentNullException(nameof(merchPack),
                            $"{nameof(merchPack)} must be provided");
            Status = status
                        ?? throw new ArgumentNullException(nameof(status),
                            $"{nameof(status)} must be provided");
        }
        
        public Id EmployeeId { get; }
        public Email EmployeeNotificationEmail { get; }
        public ClothingSize EmployeeClothingSize { get; }
        public MerchPack MerchPack { get; }
        public MerchRequestStatus Status { get; }
    }
}