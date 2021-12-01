using MediatR;

namespace MerchandiseService.Infrastructure.Commands.MerchRequestAggregate
{
    public class CreateMerchRequestCommand : IRequest<long>
    {
        public string EmployeeEmail { get; init; }
        public string EmployeeName { get; init; }
        public string ManagerEmail { get; init; }
        public string ManagerName { get; init; }
        public int ClothingSize { get; init; }
        public int MerchPackType { get; init; }
    }
}