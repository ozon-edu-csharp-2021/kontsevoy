using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.Base.Contracts;
using MerchandiseService.Infrastructure.Database.Postgres.Repositories.Implementation;
using MerchandiseService.Infrastructure.Database.Postgres.Repositories.Infrastructure;
using MerchandiseService.Infrastructure.Database.Repositories.Infrastructure;
using MerchandiseService.Infrastructure.Database.Repositories.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace MerchandiseService.Infrastructure.Database.Postgres.Extensions
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseComponents(this IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
        }

        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект <see cref="IServiceCollection"/></param>
        public static void AddInfrastructureRepositories(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IMerchRequestRepository, MerchRequestRepository>();
        }
    }
}