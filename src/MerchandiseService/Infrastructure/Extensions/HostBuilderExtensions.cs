using System;
using System.IO;
using System.Reflection;
using MerchandiseService.HostedServices;
using MerchandiseService.Infrastructure.Database.Postgres.Extensions;
using MerchandiseService.Infrastructure.Filters;
using MerchandiseService.Infrastructure.Interceptors;
using MerchandiseService.Infrastructure.Kafka.Extensions;
using MerchandiseService.Infrastructure.StartupFilters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace MerchandiseService.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Alive endpoints
                services.AddSingleton<IStartupFilter, AliveStartupFilter>();
                //Swagger
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "MerchandiseService", Version = "v1"});
                
                    options.CustomSchemaIds(x => x.FullName);

                    var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                    options.IncludeXmlComments(xmlFilePath);
                });
                //Infrastructure
                services.AddInfrastructureServices();
                //Database
                services.AddDatabaseComponents();
                services.AddInfrastructureRepositories();
                //Kafka
                services.AddMessageBroker();
                services.AddHostedService<StockReplenishedConsumerHostedService>();
                services.AddHostedService<EmployeeEventsConsumerHostedService>();
                //Grpc
                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
                //MerchRequest handlers
                services.AddHostedService<AcceptorHostedService>();
                services.AddHostedService<ProcessorHostedService>();
                //MemoryCache
                services.AddMemoryCache();
            });
            return builder;
        }
        
        public static IHostBuilder AddHttp(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
            });
            return builder;
        }
        
        public static IHostBuilder AddSerilog(this IHostBuilder builder)
        {
            builder.UseSerilog((context, configuration) => configuration
                .ReadFrom
                .Configuration(context.Configuration)
                .WriteTo.Console());
            return builder;
        }
    }
}