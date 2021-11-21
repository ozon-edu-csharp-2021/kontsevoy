using System;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace MerchandiseService.Infrastructure.Repositories.Models
{
    public record MerchRequestDto
    {   
        public long Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public long EmployeeId { get; init; }
        public string EmployeeNotificationEmail { get; init; }
        public string EmployeeClothingSize { get; init; }
        public long MerchPackId { get; init; }
        public string Status { get; init; }
        public DateTime? TryHandoutAt { get; init; }
        public DateTime? HandoutAt { get; init; }
        public string Handout { get; init; }

        public static MerchRequestDto From(MerchRequest merchRequest)
            => new()
            {
                Id = merchRequest.IsTransient ? default : merchRequest.Id.Value,
                CreatedAt = merchRequest.CreatedAt.Value,
                EmployeeId = merchRequest.EmployeeId.Value,
                EmployeeNotificationEmail = merchRequest.EmployeeNotificationEmail,
                EmployeeClothingSize = merchRequest.EmployeeClothingSize,
                MerchPackId = merchRequest.MerchPack.Id,
                Status = merchRequest.Status,
                TryHandoutAt = merchRequest.TryHandoutAt,
                HandoutAt = merchRequest.HandoutAt,
                Handout = merchRequest.Handout
            };
    }
}