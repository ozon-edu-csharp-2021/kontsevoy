using CSharpCourse.Core.Lib.Enums;

namespace MerchandiseService.Models
{
    public class MerchInquiryModel
    {
        public long EmployeeId { get; set; }
        public MerchType MerchPackType { get; set; } 
    }
}