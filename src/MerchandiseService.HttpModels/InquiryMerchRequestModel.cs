using CSharpCourse.Core.Lib.Enums;

namespace MerchandiseService.HttpModels
{
    public class InquiryMerchRequestModel
    {
        public long EmployeeId { get; set; }
        
        public MerchType MerchPackType { get; set; }
    }
}