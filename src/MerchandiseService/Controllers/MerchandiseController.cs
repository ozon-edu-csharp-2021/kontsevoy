using System.Linq;
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
                EmployeeEmail = request.EmployeeEmail,
                EmployeeName = request.EmployeeName,
                ManagerEmail = request.ManagerEmail,
                ManagerName = request.ManagerName,
                ClothingSize = (int)request.ClothingSize,
                MerchPackType = (int)request.MerchPackType
            };
            
            var merchRequestId = await Mediator.Send(merchRequest, token);
            
            return Ok(new RequestMerchResponse(merchRequestId));
        }
        
        /// <summary>
        /// Список запросов мерча 
        /// </summary>
        /// <param name="request">По кому</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Список запросов</returns>
        [HttpPost("story")]
        public async Task<ActionResult<StoryMerchResponse>> StoryMerch(StoryMerchRequest request, CancellationToken token)
        {
            var inquiryRequest = new StoryMerchRequestQuery
            {
                EmployeeEmail = request.EmployeeEmail
            };

            var response = await Mediator.Send(inquiryRequest, token);
            var result = new StoryMerchResponse
            (
                response.EmployeeEmail,
                response.MerchRequests.Select(f => new StoryMerchResponseItem(
                    f.EmployeeName,
                    $"{f.ManagerName} <{f.ManagerEmail}>",
                    f.Pack,
                    f.ClothingSize,
                    f.RequestedAt,
                    f.Status,
                    f.TryHandoutAt,
                    f.HandoutAt
                )).ToList().AsReadOnly()
            );
            
            return Ok(result);
        }
    }
}