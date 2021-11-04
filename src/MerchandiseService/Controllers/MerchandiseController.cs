using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.HttpClient.Models;
using MerchandiseService.Infrastructure.Commands.MerchRequestAggregate;
using MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchandiseController : ControllerBase
    {
        private IMediator Mediator { get; }

        public MerchandiseController(IMediator mediator) => Mediator = mediator;
        
        /// <summary>
        /// Сформировать запрос на выдачу комплекта мерча
        /// </summary>
        /// <param name="request">Кому необходимо выдать и какой тип комплекта мерча</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Идентификатор инициированного запроса</returns>
        [HttpPost("request")]
        public async Task<ActionResult<RequestMerchResponse>> RequestMerch(RequestMerchRequest request, CancellationToken token)
        {
            var merchRequest = new CreateMerchRequestCommand
            {
                EmployeeId = request.EmployeeId,
                NotificationEmail = request.NotificationEmail,
                ClothingSize = (int)request.ClothingSize,
                MerchPackType = (int)request.MerchPackType
            };
            
            var merchRequestId = await Mediator.Send(merchRequest, token);
            
            return Ok(new RequestMerchResponse(merchRequestId));
        }
        
        /// <summary>
        /// Проверить был ли выдан комплект мерча 
        /// </summary>
        /// <param name="request">Кому и какой тип комплекта</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Факт выдачи</returns>
        [HttpPost("inquiry")]
        public async Task<ActionResult<InquiryMerchResponse>> InquiryMerch(InquiryMerchRequest request, CancellationToken token)
        {
            var inquiryRequest = new InquiryMerchRequestQuery
            {
                EmployeeId = request.EmployeeId,
                MerchPackId = (int)request.MerchPackType
            };

            var isHandOut = await Mediator.Send(inquiryRequest, token);
            
            return Ok(new InquiryMerchResponse(isHandOut));
        }
    }
}