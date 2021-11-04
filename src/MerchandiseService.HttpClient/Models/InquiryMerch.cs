using CSharpCourse.Core.Lib.Enums;

namespace MerchandiseService.HttpClient.Models
{
    public record InquiryMerchRequest
    {
        public InquiryMerchRequest(ulong employeeId, MerchType merchPackType)
        {
            EmployeeId = employeeId;
            MerchPackType = merchPackType;
        }
        
        public ulong EmployeeId { get; }
        public MerchType MerchPackType { get; }
    }
    
    public record InquiryMerchResponse
    {
        public InquiryMerchResponse(bool handOut) => HandOut = handOut;
        public bool HandOut { get; }
    }
}