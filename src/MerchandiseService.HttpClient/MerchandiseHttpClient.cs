using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpClient.Models;
using RestEase;

namespace MerchandiseService.HttpClient
{
    [BasePath("v1/api/merch")]
    public interface IMerchandiseHttpClient
    {
        [Post("request")]
        Task<RequestMerchResponse> RequestMerch([Body] RequestMerchRequest request, CancellationToken token);
        [Post("story")]
        Task<StoryMerchResponse> InquiryMerch([Body] StoryMerchRequest request, CancellationToken token);
    }
}