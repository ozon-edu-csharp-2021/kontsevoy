using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class StoryMerchRequestQueryHandler : IRequestHandler<StoryMerchRequestQuery, StoryMerchRequestQueryResponse>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }
        private ITracer Tracer { get; }

        public StoryMerchRequestQueryHandler(IMerchRequestRepository merchRequestRepository, ITracer tracer) =>
            (MerchRequestRepository, Tracer) = (merchRequestRepository, tracer);
        
        public async Task<StoryMerchRequestQueryResponse> Handle(StoryMerchRequestQuery request, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(StoryMerchRequestQueryHandler)).StartActive();
            
            var employeeEmail = (Email)request.EmployeeEmail;
            var items = await MerchRequestRepository.FindByEmployeeEmailAsync(employeeEmail, cancellationToken);
            var result = new StoryMerchRequestQueryResponse
            {
                EmployeeEmail = employeeEmail,
                MerchRequests = items.Select(f => new StoryMerchRequestQueryResponseItem
                {
                    EmployeeName = f.EmployeeName,
                    ManagerEmail = f.ManagerEmail,
                    ManagerName = f.ManagerName,
                    Pack = f.MerchPack,
                    ClothingSize = f.EmployeeClothingSize,
                    RequestedAt = f.CreatedAt.Value,
                    Status = f.Status,
                    TryHandoutAt = f.TryHandoutAt,
                    HandoutAt = f.HandoutAt
                }).ToList().AsReadOnly()
            };
            return result;
        }
    }
}