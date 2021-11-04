using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using MerchandiseService.Grpc;
using MerchandiseService.Infrastructure.Commands.MerchRequestAggregate;
using MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        private IMediator Mediator { get; set; }

        public MerchandiseGrpcService(IMediator mediator) => Mediator = mediator;
        
        public override async Task<RequestMerchResponse> RequestMerch(RequestMerchRequest request, ServerCallContext context)
        {
            var requestId = await Mediator.Send(new CreateMerchRequestCommand
            {
                EmployeeId = request.EmployeeId,
                NotificationEmail = request.NotificationEmail,
                ClothingSize = (int)request.ClothingSize,
                MerchPackType = (int)request.MerchPackType
            }, context.CancellationToken);
            
            return new RequestMerchResponse
            {
                MerchRequestId = requestId
            };
        }

        public override async Task<InquiryMerchResponse> InquiryMerch(InquiryMerchRequest request, ServerCallContext context)
        {
            var isHandOut = await Mediator.Send(new InquiryMerchRequestQuery
            {
                EmployeeId = request.EmployeeId,
                MerchPackId = (int)request.MerchPackType
            }, context.CancellationToken);
            
            return new InquiryMerchResponse
            {
                HandOut = isHandOut
            };
        }
    }
}