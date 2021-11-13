using CSharpCourse.Core.Lib.Enums;

namespace MerchandiseService.HttpClient.Models
{
    public record InquiryMerchRequest
    {
        public InquiryMerchRequest(long employeeId, MerchType merchPackType)
        {
            EmployeeId = employeeId;
            MerchPackType = merchPackType;
        }
        
        public long EmployeeId { get; }
        public MerchType MerchPackType { get; }
    }
    
    public record InquiryMerchResponse
    {
        public InquiryMerchResponse(bool handOut) => HandOut = handOut;
        public bool HandOut { get; }
    }
}