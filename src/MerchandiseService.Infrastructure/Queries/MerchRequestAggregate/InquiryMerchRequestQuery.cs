using MediatR;

namespace MerchandiseService.Infrastructure.Queries.MerchRequestAggregate
{
    public class InquiryMerchRequestQuery : IRequest<bool>
    {
        public ulong EmployeeId { get; init; }
        public int MerchPackId { get; init; }
    }
}