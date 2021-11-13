using MediatR;

namespace MerchandiseService.Infrastructure.Commands.MerchRequestAggregate
{
    public class CreateMerchRequestCommand : IRequest<long>
    {
        public long EmployeeId { get; init; }
        public string NotificationEmail { get; init; }
        public int ClothingSize { get; init; }
        public int MerchPackType { get; init; }
    }
}