using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using MediatR;
using MerchandiseService.Infrastructure.Configuration;
using MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Extensions
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных сервисов
        /// </summary>
        /// <param name="services">Объект <see cref="IServiceCollection"/></param>
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateMerchRequestCommandHandler).Assembly);
        }
        
        public static void AddJaeger(this IServiceCollection services, IConfiguration configuration)
        {
            // Adds the Jaeger Tracer.
            var address = configuration.GetSection(nameof(JaegerConfiguration))
                .Get<JaegerConfiguration>()?.ServerAddress;
            
            services.AddSingleton<ITracer>(sp =>
            {
                var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender(address, 0, 0))
                    .Build();
                var tracer = new Tracer.Builder(serviceName)
                    .WithSampler(new ConstSampler(true))
                    .WithReporter(reporter)
                    .Build();
                return tracer;
            });
        }
    }
}