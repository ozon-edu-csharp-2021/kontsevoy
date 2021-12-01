using MediatR;
using MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMediatR(typeof(InquiryMerchRequestQueryHandler).Assembly);
            services.AddMediatR(typeof(ReplenishStockCommandHandler).Assembly);
        }
    }
}