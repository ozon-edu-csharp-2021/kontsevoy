﻿using CSharpCourse.Core.Lib.Enums;
using MerchandiseService.HttpClient.Enums;

namespace MerchandiseService.HttpClient.Models
{
    public record RequestMerchRequest
    {
        public RequestMerchRequest(
            ulong employeeId, 
            string notificationEmail, 
            ClothingSizeEnum clothingSize, 
            MerchType merchPackType)
        {
            EmployeeId = employeeId;
            NotificationEmail = notificationEmail;
            ClothingSize = clothingSize;
            MerchPackType = merchPackType;
        }
        
        public ulong EmployeeId { get; }
        public string NotificationEmail { get; }
        public ClothingSizeEnum ClothingSize { get; }
        public MerchType MerchPackType { get; }
    }
    
    public record RequestMerchResponse
    {
        public RequestMerchResponse(ulong merchRequestId) => MerchRequestId = merchRequestId;
        public ulong MerchRequestId { get; }
    }
}