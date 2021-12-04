using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using MerchandiseService.Infrastructure.Commands.EmployeeEvents;
using MerchandiseService.Infrastructure.Kafka.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MerchandiseService.HostedServices
{
    public class EmployeeEventsConsumerHostedService : BackgroundService
    {
        private KafkaConfiguration KafkaConfiguration { get; }
        private IServiceScopeFactory ScopeFactory { get; }
        private ILogger<EmployeeEventsConsumerHostedService> Logger { get; }

        public EmployeeEventsConsumerHostedService(
            IOptions<KafkaConfiguration> config,
            IServiceScopeFactory scopeFactory,
            ILogger<EmployeeEventsConsumerHostedService> logger)
        {
            KafkaConfiguration = config.Value;
            ScopeFactory = scopeFactory;
            Logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = KafkaConfiguration.EmployeeNotificationGroup,
                BootstrapServers = KafkaConfiguration.BootstrapServers,
            };
            
            Logger.LogInformation("EmployeeEventsConsumer listening {server} on {topic}",
                KafkaConfiguration.BootstrapServers, KafkaConfiguration.EmployeeNotificationTopic);

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(KafkaConfiguration.EmployeeNotificationTopic);
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
                            var message = JsonSerializer.Deserialize<NotificationEvent>(consume.Message.Value);
                            if (message is null) throw new JsonException($"Deserializer return null as result");
                            int? ClothingSize = null;
                            int? MerchType = null;
                            if (message.Payload is JsonElement element)
                            {
                                if (element.TryGetProperty("ClothingSize", out var clothingSize)
                                    && clothingSize.TryGetInt32(out var clothingSizeIntValue))
                                    ClothingSize = clothingSizeIntValue;
                                if (element.TryGetProperty("MerchType", out var merchType)
                                    && merchType.TryGetInt32(out var merchTypeIntValue))
                                    MerchType = merchTypeIntValue;
                            }
                            var payload = new EmployeeEventPayload
                            {
                                EmployeeEmail = message.EmployeeEmail,
                                EmployeeName = message.EmployeeName,
                                ManagerEmail = message.ManagerEmail,
                                ManagerName = message.ManagerName,
                                ClothingSize = ClothingSize,
                                MerchType = MerchType,
                            };
                            
                            switch (message.EventType)
                            {
                                case EmployeeEventType.Hiring:
                                    await mediator.Send(new EmployeeHiredCommand(payload), stoppingToken);
                                    break;
                                case EmployeeEventType.ProbationPeriodEnding:
                                    await mediator.Send(new EmployeeProbationPeriodEndingCommand(payload), stoppingToken);
                                    break;
                                case EmployeeEventType.ConferenceAttendance:
                                    await mediator.Send(new EmployeeConferenceAttendanceCommand(payload), stoppingToken);
                                    break;
                                case EmployeeEventType.Dismissal:
                                    await mediator.Send(new EmployeeDismissalCommand(payload), stoppingToken);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException($"Unhandled employee event type {Enum.GetName(message.EventType)}");
                            }
                            
                            
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