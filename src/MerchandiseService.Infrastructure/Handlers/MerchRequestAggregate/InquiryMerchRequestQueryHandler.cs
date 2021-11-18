using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class InquiryMerchRequestQueryHandler :  IRequestHandler<InquiryMerchRequestQuery, bool>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }

        public InquiryMerchRequestQueryHandler(IMerchRequestRepository merchRequestRepository) =>
            MerchRequestRepository = merchRequestRepository;
        
        public async Task<bool> Handle(InquiryMerchRequestQuery request, CancellationToken cancellationToken)
        {
            var employeeId = new Id(request.EmployeeId);
            var merchPack = MerchPack.GetById(request.MerchPackId);
            return await MerchRequestRepository.ContainsByParamsAsync(employeeId, merchPack, cancellationToken);
        }
    }
}