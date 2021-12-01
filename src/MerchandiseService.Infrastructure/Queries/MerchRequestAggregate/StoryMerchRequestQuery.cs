using System;
using System.Collections.Generic;
using MediatR;

namespace MerchandiseService.Infrastructure.Queries.MerchRequestAggregate
{
    public class StoryMerchRequestQuery : IRequest<StoryMerchRequestQueryResponse>
    {
        public string EmployeeEmail { get; init; }
    }

    public class StoryMerchRequestQueryResponse
    {
        public string EmployeeEmail { get; init; }
        public IReadOnlyCollection<StoryMerchRequestQueryResponseItem> MerchRequests { get; init; }
    }
    
    public record StoryMerchRequestQueryResponseItem
    {
        public string EmployeeName { get; init; }
        public string ManagerEmail { get; init; }
        public string ManagerName { get; init; }
        public string Pack { get; init; }
        public string ClothingSize { get; init; }
        public DateTime RequestedAt { get; init; }
        public string Status { get; init; }
        public DateTime? TryHandoutAt { get; init; }
        public DateTime? HandoutAt { get; init; }
    }
}