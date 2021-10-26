using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpModels;
using SysHttpClient = System.Net.Http.HttpClient;

namespace MerchandiseService.HttpClient
{
    public interface IMerchandiseHttpClient
    {
        Task<RequestMerchResponseModel> RequestMerch(RequestMerchRequestModel request, CancellationToken token);
        Task<InquiryMerchResponseModel> InquiryMerch(InquiryMerchRequestModel request, CancellationToken token);
    }
    
    public class MerchandiseHttpClient : IMerchandiseHttpClient
    {
        private SysHttpClient HttpClient { get; }

        public MerchandiseHttpClient(SysHttpClient httpClient) => HttpClient = httpClient;

        private async Task<R> PostAsync<T, R>(string requestUri, T request, CancellationToken token)
        {
            var requestBody = JsonSerializer.Serialize(request);
            var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
            using var response = await HttpClient.PostAsync(requestUri, requestContent, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<R>(body);
        }

        public async Task<RequestMerchResponseModel> RequestMerch(RequestMerchRequestModel request, CancellationToken token) =>
            await PostAsync<RequestMerchRequestModel, RequestMerchResponseModel>("v1/api/merch/request", request, token);

        public async Task<InquiryMerchResponseModel> InquiryMerch(InquiryMerchRequestModel request, CancellationToken token) =>
            await PostAsync<InquiryMerchRequestModel, InquiryMerchResponseModel>("v1/api/merch/inquiry", request, token);
    }
}