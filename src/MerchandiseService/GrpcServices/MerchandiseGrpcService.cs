using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using Grpc.Core;
using MerchandiseService.Grpc;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;

namespace MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        private IMerchandiseService MerchandiseService { get; set; }

        public MerchandiseGrpcService(IMerchandiseService merchandiseService) => MerchandiseService = merchandiseService;
        
        public override async Task<RequestMerchResponse> RequestMerch(RequestMerchRequest request, ServerCallContext context)
        {
            var requestId = await MerchandiseService.CreateMerchRequest(new MerchRequestCreationModel
            {
                EmployeeId = request.EmployeeId,
                MerchPackType = (MerchType)request.MerchPackType
            }, context.CancellationToken);
            return new RequestMerchResponse
            {
                MerchRequestId = requestId
            };
        }

        public override async Task<InquiryMerchResponse> InquiryMerch(InquiryMerchRequest request, ServerCallContext context)
        {
            var isHandOut = await MerchandiseService.InquiryMerch(new MerchInquiryModel
            {
                EmployeeId = request.EmployeeId,
                MerchPackType = (MerchType)request.MerchPackType
            }, context.CancellationToken);
            return new InquiryMerchResponse
            {
                HandOut = isHandOut
            };
        }
    }
}