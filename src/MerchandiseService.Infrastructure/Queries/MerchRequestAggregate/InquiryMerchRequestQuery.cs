using MediatR;

namespace MerchandiseService.Infrastructure.Queries.MerchRequestAggregate
{
    public class InquiryMerchRequestQuery : IRequest<bool>
    {
        public long EmployeeId { get; init; }
        public int MerchPackId { get; init; }
    }
}