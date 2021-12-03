using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Base.Models;
using MerchandiseService.Domain.Exceptions;

namespace MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequest : Entity<Id>
    {
        public static MerchRequest New(
            CreationTimestamp createdAt,
            Email employeeEmail,
            Name employeeName,
            Email managerEmail,
            Name managerName,
            ClothingSize employeeClothingSize,
            MerchPack merchPack) =>
            new(null,
                createdAt,
                employeeEmail,
                employeeName,
                managerEmail,
                managerName,
                employeeClothingSize, 
                merchPack,
                MerchRequestStatus.New);
        
        private MerchRequest(
            Id id,
            CreationTimestamp createdAt,
            Email employeeEmail,
            Name employeeName,
            Email managerEmail,
            Name managerName,
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
            EmployeeEmail = employeeEmail 
                            ?? throw new ArgumentNullException(nameof(employeeEmail),
                                $"{nameof(employeeEmail)} must be provided");
            EmployeeName = employeeName 
                            ?? throw new ArgumentNullException(nameof(employeeName),
                                $"{nameof(employeeName)} must be provided");
            ManagerEmail = managerEmail 
                           ?? throw new ArgumentNullException(nameof(managerEmail),
                               $"{nameof(managerEmail)} must be provided");
            ManagerName = managerName 
                           ?? throw new ArgumentNullException(nameof(managerName),
                               $"{nameof(managerName)} must be provided");
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
        
        public CreationTimestamp CreatedAt { get; init; }
        public Email EmployeeEmail { get; }
        public Name EmployeeName { get; }
        public Email ManagerEmail { get; }
        public Name ManagerName { get; }
        public ClothingSize EmployeeClothingSize { get; }
        public MerchPack MerchPack { get; }
        public MerchRequestStatus Status { get; private set; }
        public HandoutTimestamp TryHandoutAt { get; private set; }
        public HandoutTimestamp HandoutAt { get; private set; }
        public Handout Handout { get; private set; }

        public void ReadyToProcessing()
        {
            if (Status != MerchRequestStatus.New || Status != MerchRequestStatus.Awaiting)
                throw new MerchRequestStatusException(
                    $"Change status to {MerchRequestStatus.Processing} from {Status} unavailable.");
            
            Status = MerchRequestStatus.Processing;
        }

        public void DoHandout(Handout handout, HandoutTimestamp timestamp)
        {
            if (Status != MerchRequestStatus.Processing)
                throw new MerchRequestStatusException(
                    $"Handout available only in status {MerchRequestStatus.Processing}. Current status {Status}");
            
            Handout = handout ?? throw new ArgumentNullException(nameof(handout), $"{nameof(handout)} must be provided");
            HandoutAt = TryHandoutAt = timestamp ?? throw new ArgumentNullException(nameof(timestamp),
                $"{nameof(timestamp)} must be provided");
            
            Status = MerchRequestStatus.Done;
        }

        public void TryHandoutNeedAwait(HandoutTimestamp timestamp)
        {
            if (Status != MerchRequestStatus.Processing)
                throw new MerchRequestStatusException(
                    $"TryHandoutNeedAwait available only in status {MerchRequestStatus.Processing}. Current status {Status}");
            
            TryHandoutAt = timestamp ?? throw new ArgumentNullException(nameof(timestamp),
                $"{nameof(timestamp)} must be provided");
            
            Status = MerchRequestStatus.Awaiting;
        }
    }
}