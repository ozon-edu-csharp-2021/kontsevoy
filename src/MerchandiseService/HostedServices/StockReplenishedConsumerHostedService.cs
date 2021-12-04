using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using MerchandiseService.Infrastructure.Commands.ReplenishStockEvent;
using MerchandiseService.Infrastructure.Kafka.Configuration;
using MerchandiseService.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MerchandiseService.HostedServices
{
    public class StockReplenishedConsumerHostedService : BackgroundService
    {
        private KafkaConfiguration KafkaConfiguration { get; }
        private IServiceScopeFactory ScopeFactory { get; }
        private ILogger<StockReplenishedConsumerHostedService> Logger { get; }

        public StockReplenishedConsumerHostedService(
            IOptions<KafkaConfiguration> config,
            IServiceScopeFactory scopeFactory,
            ILogger<StockReplenishedConsumerHostedService> logger)
        {
            KafkaConfiguration = config.Value;
            ScopeFactory = scopeFactory;
            Logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = KafkaConfiguration.ReplenishedGroup,
                BootstrapServers = KafkaConfiguration.BootstrapServers,
            };
            
            Logger.LogInformation("StockReplenishedConsumer listening {server} on {topic}",
                KafkaConfiguration.BootstrapServers, KafkaConfiguration.ReplenishedTopic);

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(KafkaConfiguration.ReplenishedTopic);
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var scope = ScopeFactory.CreateScope();
                    try
                    {
                        await Task.Yield();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var consume = consumer.Consume(stoppingToken);
                        if (consume != null)
                        {
                            var message = JsonSerializer.Deserialize<StockReplenishedEvent>(consume.Message.Value);
                            if (message is null) throw new JsonException($"Deserializer return null as result");
                            await mediator.Send(new ReplenishStockCommand()
                            {
                                Items = message.Type.Select(it => new StockItemDto()
                                {
                                    Sku = it.Sku,
                                    ItemTypeId = it.ItemTypeId,
                                    ItemTypeName = it.ItemTypeName,
                                    ClothingSize = it.ClothingSize
                                }).ToArray()
                            }, stoppingToken);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error while get consume. Message {message}", ex.Message);
                    }
                }
            }
            finally
            {
                consumer.Commit();
                consumer.Close();
            }
        }
    }
}