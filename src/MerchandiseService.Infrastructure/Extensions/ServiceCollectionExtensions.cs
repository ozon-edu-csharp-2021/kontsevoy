using MediatR;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Infrastructure.Handlers.EmployeeAggregate;
using MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;
using MerchandiseService.Infrastructure.Stubs;
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
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateOrUpdateEmployeeCommandHandler).Assembly);
            services.AddMediatR(typeof(CreateMerchRequestCommandHandler).Assembly);
            services.AddMediatR(typeof(InquiryMerchRequestQueryHandler).Assembly);
            
            return services;
        }

        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWorkStub>();
            services.AddTransient<IEmployeeRepository, EmployeeRepositoryStub>();
            services.AddTransient<IMerchRequestRepository, MerchRequestRepositoryStub>();
            
            return services;
        }
    }
}