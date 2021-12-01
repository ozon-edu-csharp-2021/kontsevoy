using System;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace MerchandiseService.Infrastructure.Database.Postgres.Repositories.Models
{
    public record MerchRequestDto
    {   
        public long Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public string EmployeeEmail { get; init; }
        public string EmployeeName { get; init; }
        public string ManagerEmail { get; init; }
        public string ManagerName { get; init; }
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
                EmployeeEmail = merchRequest.EmployeeEmail,
                EmployeeName = merchRequest.EmployeeName,
                ManagerEmail = merchRequest.ManagerEmail,
                ManagerName = merchRequest.ManagerName,
                EmployeeClothingSize = merchRequest.EmployeeClothingSize,
                MerchPackId = merchRequest.MerchPack.Id,
                Status = merchRequest.Status,
                TryHandoutAt = merchRequest.TryHandoutAt,
                HandoutAt = merchRequest.HandoutAt,
                Handout = merchRequest.Handout
            };
    }
}