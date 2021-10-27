using CSharpCourse.Core.Lib.Enums;

namespace MerchandiseService.HttpClient.Models
{
    public record RequestMerchRequest
    {
        public RequestMerchRequest(long employeeId, MerchType merchPackType)
        {
            EmployeeId = employeeId;
            MerchPackType = merchPackType;
        }
        
        public long EmployeeId { get; }
        public MerchType MerchPackType { get; }
    }
    
    public record RequestMerchResponse
    {
        public RequestMerchResponse(long merchRequestId) => MerchRequestId = merchRequestId;
        public long MerchRequestId { get; }
    }
}