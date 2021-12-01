using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class StoryMerchRequestQueryHandler :  IRequestHandler<StoryMerchRequestQuery, StoryMerchRequestQueryResponse>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }

        public StoryMerchRequestQueryHandler(IMerchRequestRepository merchRequestRepository) =>
            MerchRequestRepository = merchRequestRepository;
        
        public async Task<StoryMerchRequestQueryResponse> Handle(StoryMerchRequestQuery request, CancellationToken cancellationToken)
        {
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