using System;
using System.Collections.Generic;

namespace MerchandiseService.HttpClient.Models
{
    public record StoryMerchRequest
    {
        public StoryMerchRequest(string employeeEmail)
        {
            EmployeeEmail = employeeEmail;
        }
        
        public string EmployeeEmail { get; }
    }
    
    public record StoryMerchResponse
    {
        public StoryMerchResponse(string employeeEmail, IReadOnlyCollection<StoryMerchResponseItem> requests)
            => (EmployeeEmail, Requests, RequestsCount) = (employeeEmail, requests, requests.Count);
        
        public string EmployeeEmail { get; }
        
        public int RequestsCount { get; }
        public IReadOnlyCollection<StoryMerchResponseItem> Requests { get; }
    }

    public record StoryMerchResponseItem
    {
        public StoryMerchResponseItem(string employeeName, string manager, string pack, string clothingSize,
            DateTime requestedAt, string status, DateTime? tryHandoutAt, DateTime? handoutAt)
        {
            EmployeeName = employeeName;
            Manager = manager;
            Pack = pack;
            ClothingSize = clothingSize;
            RequestedAt = requestedAt;
            Status = status;
            TryHandoutAt = tryHandoutAt;
            HandoutAt = handoutAt;
        }
        
        public string EmployeeName { get; }
        public string Manager { get; }
        public string Pack { get; }
        public string ClothingSize { get; }
        public DateTime RequestedAt { get; }
        public string Status { get; }
        public DateTime? TryHandoutAt { get; }
        public DateTime? HandoutAt { get; }
    }
}