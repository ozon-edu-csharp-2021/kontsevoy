using CSharpCourse.Core.Lib.Enums;

namespace MerchandiseService.Models
{
    public class MerchRequestCreationModel
    {
        public long EmployeeId { get; set; }
        
        public MerchType MerchPackType { get; set; }
    }
}