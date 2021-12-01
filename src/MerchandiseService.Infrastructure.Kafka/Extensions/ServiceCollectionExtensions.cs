using MerchandiseService.Infrastructure.Kafka.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MerchandiseService.Infrastructure.Kafka.Extensions
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void AddKafkaConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaConfiguration>(configuration);
        }
        
        /// <summary>
        /// Добавление в DI контейнер брокера сообщений
        /// </summary>
        /// <param name="services">Объект <see cref="IServiceCollection"/></param>
        public static void AddMessageBroker(this IServiceCollection services)
        {
            
        }
    }
}