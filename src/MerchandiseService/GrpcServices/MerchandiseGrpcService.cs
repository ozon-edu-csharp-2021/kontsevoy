using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using MerchandiseService.Grpc;
using MerchandiseService.Infrastructure.Commands.MerchRequestAggregate;
using MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;
using OpenTracing;

namespace MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        private IMediator Mediator { get; }
        
        private ITracer Tracer { get; }

        public MerchandiseGrpcService(IMediator mediator, ITracer tracer) => (Mediator, Tracer) = (mediator, tracer);
        
        public override async Task<RequestMerchResponse> RequestMerch(RequestMerchRequest request, ServerCallContext context)
        {
            using var span = Tracer.BuildSpan(nameof(RequestMerch)).WithTag(nameof(request.EmployeeEmail), request.EmployeeEmail).StartActive();
            
            var requestId = await Mediator.Send(new CreateMerchRequestCommand
            {
                EmployeeEmail = request.EmployeeEmail,
                EmployeeName = request.EmployeeName,
                ManagerEmail = request.ManagerEmail,
                ManagerName = request.ManagerName,
                ClothingSize = (int)request.ClothingSize,
                MerchPackType = (int)request.MerchPackType
            }, context.CancellationToken);
            
            return new RequestMerchResponse
            {
                MerchRequestId = requestId
            };
        }

        public override async Task<StoryMerchResponse> StoryMerch(StoryMerchRequest request, ServerCallContext context)
        {
            using var span = Tracer.BuildSpan(nameof(StoryMerch)).WithTag(nameof(request.EmployeeEmail), request.EmployeeEmail).StartActive();
            
            var response = await Mediator.Send(new StoryMerchRequestQuery
            {
                EmployeeEmail = request.EmployeeEmail
            }, context.CancellationToken);
            
            var result = new StoryMerchResponse
            {
                EmployeeEmail = response.EmployeeEmail
            };
            result.Requests.AddRange(response.MerchRequests.Select(
                f => new StoryMerchResponseItem
                {
                    EmployeeName = f.EmployeeName,
                    Manager = $"{f.ManagerName} <{f.ManagerEmail}>",
                    Pack = f.Pack,
                    ClothingSize = f.ClothingSize,
                    RequestedAt = f.RequestedAt.ToShortDateString(),
                    Status = f.Status,
                    TryHandoutAt = f.TryHandoutAt?.ToShortDateString(),
                    HandoutAt = f.HandoutAt?.ToShortDateString()
                }));
            result.RequestsCount = result.Requests.Count;
            
            return result;
        }
    }
}