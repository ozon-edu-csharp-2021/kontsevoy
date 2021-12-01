using System;
using Grpc.Net.Client;
using MerchandiseService.Infrastructure.ExternalServices.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.StockApi.Grpc;

namespace MerchandiseService.Infrastructure.ExternalServices.Extensions
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление в DI контейнер внешних сервисов
        /// </summary>
        /// <param name="services">Объект <see cref="IServiceCollection"/></param>
        /// <param name="configuration">Representation of key/value application configuration properties</param>
        public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStockApiService(configuration);
        }

        private static void AddStockApiService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionAddress = configuration.GetSection(nameof(StockApiGrpcServiceConfiguration))
                .Get<StockApiGrpcServiceConfiguration>().ServerAddress;

            if (string.IsNullOrWhiteSpace(connectionAddress))
                throw new ApplicationException($"Wrong or empty {nameof(StockApiGrpcServiceConfiguration)}");

            services.AddScoped(_ => new StockApiGrpc.StockApiGrpcClient(GrpcChannel.ForAddress(connectionAddress)));
        }
    }
}