using CSharpCourse.Core.Lib.Enums;
using MerchandiseService.HttpClient.Enums;

namespace MerchandiseService.HttpClient.Models
{
    public record RequestMerchRequest
    {
        public RequestMerchRequest(
            string employeeEmail,
            string employeeName,
            string managerEmail,
            string managerName,
            ClothingSizeEnum clothingSize, 
            MerchType merchPackType)
        {
            EmployeeEmail = employeeEmail;
            EmployeeName = employeeName;
            ManagerEmail = managerEmail;
            ManagerName = managerName;
            ClothingSize = clothingSize;
            MerchPackType = merchPackType;
        }
        
        public string EmployeeEmail { get; }
        public string EmployeeName { get; }
        public string ManagerEmail { get; }
        public string ManagerName { get; }
        public ClothingSizeEnum ClothingSize { get; }
        public MerchType MerchPackType { get; }
    }
    
    public record RequestMerchResponse
    {
        public RequestMerchResponse(long merchRequestId) => MerchRequestId = merchRequestId;
        public long MerchRequestId { get; }
    }
}