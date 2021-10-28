using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpClient.Models;
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

        public MerchandiseController(IMerchandiseService merchandiseService) => MerchandiseService = merchandiseService;
        
        /// <summary>
        /// Сформировать запрос на выдачу комплекта мерча
        /// </summary>
        /// <param name="requestMerchRequest">Кому необходимо выдать и какой тип комплекта мерча</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Идентификатор инициированного запроса</returns>
        [HttpPost("request")]
        public async Task<ActionResult<RequestMerchResponse>> RequestMerch(RequestMerchRequest requestMerchRequest, CancellationToken token)
        {
            var merchRequestId = await MerchandiseService.CreateMerchRequest(new MerchRequestCreationModel
            {
                EmployeeId = requestMerchRequest.EmployeeId,
                MerchPackType = requestMerchRequest.MerchPackType
            }, token);
            return Ok(new RequestMerchResponse(merchRequestId));
        }
        
        /// <summary>
        /// Проверить был ли выдан комплект мерча 
        /// </summary>
        /// <param name="inquiryMerchRequest">Кому и какой тип комплекта</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Факт выдачи</returns>
        [HttpPost("inquiry")]
        public async Task<ActionResult<InquiryMerchResponse>> InquiryMerch(InquiryMerchRequest inquiryMerchRequest, CancellationToken token)
        {
            var isHandOut = await MerchandiseService.InquiryMerch(new MerchInquiryModel
            {
                EmployeeId = inquiryMerchRequest.EmployeeId,
                MerchPackType = inquiryMerchRequest.MerchPackType
            }, token);
            return Ok(new InquiryMerchResponse(isHandOut));
        }
    }
}