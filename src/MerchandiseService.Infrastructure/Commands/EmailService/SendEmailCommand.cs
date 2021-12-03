using MediatR;

namespace MerchandiseService.Infrastructure.Commands.EmailService
{
    public class SendEmailCommand : IRequest
    {   
        public long Id { get; init; }
        public string EmployeeEmail { get; init; }
        public string EmployeeName { get; init; }
        public string ManagerEmail { get; init; }
        public string ManagerName { get; init; }
        public int ClothingSize { get; init; }
        public int MerchPackType { get; init; }
    }
}