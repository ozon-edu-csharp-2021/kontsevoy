using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private ILogger<LoggingInterceptor> Logger { get; }
        private ITracer Tracer { get; }

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger, ITracer tracer) => (Logger, Tracer) = (logger, tracer);

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            using var span = Tracer.BuildSpan(nameof(UnaryServerHandler)).StartActive();
            
            var requestJson = JsonSerializer.Serialize(request);
            Logger.LogInformation(requestJson);
            
            var response = base.UnaryServerHandler(request, context, continuation);
            
            string responseJson;
            if (response.IsFaulted)
            {
                responseJson = JsonSerializer.Serialize(
                        new
                        {
                            response.Exception?.InnerException?.Message,
                            Type = response.Exception?.InnerException?.GetType().FullName,
                            response.Exception?.InnerException?.Source,
                            response.Exception?.InnerException?.StackTrace
                        }
                    );
            }
            else
                responseJson = JsonSerializer.Serialize(response);
            Logger.LogInformation(responseJson);
            
            return response;
        }
    }
}