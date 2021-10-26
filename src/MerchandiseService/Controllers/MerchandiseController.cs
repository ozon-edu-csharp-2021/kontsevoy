using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpModels;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchandiseController : ControllerBase
    {
        private IMerchandiseService MerchandiseService { get; }

        public MerchandiseController(IMerchandiseService merchandiseService)
        {
            MerchandiseService = merchandiseService;
        }
        
        [HttpPost("request")]
        public async Task<ActionResult<RequestMerchResponseModel>> RequestMerch(RequestMerchRequestModel requestMerchRequest, CancellationToken token)
        {
            var merchRequestId = await MerchandiseService.CreateMerchRequest(new MerchRequestCreationModel
            {
                EmployeeId = requestMerchRequest.EmployeeId,
                MerchPackType = requestMerchRequest.MerchPackType
            }, token);
            return Ok(new RequestMerchResponseModel
            {
                RequestMerchId = merchRequestId
            });
        }
        
        [HttpPost("inquiry")]
        public async Task<ActionResult<InquiryMerchResponseModel>> InquiryMerch(InquiryMerchRequestModel inquiryMerchRequest, CancellationToken token)
        {
            var isHandOut = await MerchandiseService.InquiryMerch(new MerchInquiryModel
            {
                EmployeeId = inquiryMerchRequest.EmployeeId,
                MerchPackType = inquiryMerchRequest.MerchPackType
            }, token);
            return Ok(new InquiryMerchResponseModel
            {
                HandOut = isHandOut
            });
        }
    }
}