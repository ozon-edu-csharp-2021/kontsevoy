using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequest : Entity<Id>
    {
        public static MerchRequest New(
            CreationTimestamp createdAt,
            Id employeeId,
            Email employeeNotificationEmail,
            ClothingSize employeeClothingSize,
            MerchPack merchPack) =>
            new(null,
                createdAt,
                employeeId,
                employeeNotificationEmail,
                employeeClothingSize, 
                merchPack,
                MerchRequestStatus.New);
        
        private MerchRequest(
            Id id,
            CreationTimestamp createdAt,
            Id employeeId,
            Email employeeNotificationEmail,
            ClothingSize employeeClothingSize,
            MerchPack merchPack,
            MerchRequestStatus status,
            HandoutTimestamp tryHandoutAt = null,
            HandoutTimestamp handoutAt = null,
            Handout handout = null)
        {
            Id = id;
            CreatedAt = createdAt
                        ?? throw new ArgumentNullException(nameof(createdAt),
                            $"{nameof(createdAt)} must be provided");
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
            TryHandoutAt = tryHandoutAt;
            HandoutAt = handoutAt;
            Handout = handout;
        }
        
        public Id EmployeeId { get; }
        public CreationTimestamp CreatedAt { get; init; }
        public Email EmployeeNotificationEmail { get; }
        public ClothingSize EmployeeClothingSize { get; }
        public MerchPack MerchPack { get; }
        public MerchRequestStatus Status { get; private set; }
        public HandoutTimestamp TryHandoutAt { get; }
        public HandoutTimestamp HandoutAt { get; }
        public Handout Handout { get; }

        private static bool IsFinalStatus(MerchRequestStatus status)
            => status == MerchRequestStatus.Done || status == MerchRequestStatus.Decline;

        public void ChangeStatus(MerchRequestStatus newStatus)
        {
            if (IsFinalStatus(Status))
                throw new MerchRequestStatusException($"Request in final status. Change status unavailable");

            Status = newStatus;
        }
    }
}