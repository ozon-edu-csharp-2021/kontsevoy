using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private ILogger<LoggingInterceptor> Logger { get; }

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger) => Logger = logger;

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var requestJson = JsonSerializer.Serialize(request);
            Logger.LogInformation(requestJson);
            
            var response = base.UnaryServerHandler(request, context, continuation);

            var responseJson = JsonSerializer.Serialize(response);
            Logger.LogInformation(responseJson);
            
            return response;
        }
    }
}