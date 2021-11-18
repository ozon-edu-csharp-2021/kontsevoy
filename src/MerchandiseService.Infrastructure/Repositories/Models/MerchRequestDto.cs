using System;

namespace MerchandiseService.Infrastructure.Repositories.Models
{
    public class MerchRequestDto
    {   
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeNotificationEmail { get; set; }
        public string EmployeeClothingSize { get; set; }
        public long MerchPackId { get; set; }
        public string Status { get; set; }
        public DateTime TryHandoutAt { get; set; }
        public string Handout { get; set; }
    }
}