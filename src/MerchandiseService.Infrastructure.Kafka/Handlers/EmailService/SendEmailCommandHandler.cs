using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using MerchandiseService.Infrastructure.Commands.EmailService;
using MerchandiseService.Infrastructure.Kafka.MessageBroker;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Kafka.Handlers.EmailService
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
    {
        private IProducerBuilderWrapper ProducerBuilder { get; }
        private ILogger<SendEmailCommandHandler> Logger { get; }
        private ITracer Tracer { get; }

        public SendEmailCommandHandler(IProducerBuilderWrapper producerBuilder, ITracer tracer,
            IMediator mediator, ILogger<SendEmailCommandHandler> logger) =>
            (ProducerBuilder, Tracer, Logger) = (producerBuilder, tracer, logger);
        
        public Task<Unit> Handle(SendEmailCommand command, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(SendEmailCommandHandler)).StartActive();

            ProducerBuilder.Producer.Produce(ProducerBuilder.EmailNotificationTopic,
                new Message<string, string>()
                {
                    Key = command.Id.ToString(),
                    Value = JsonSerializer.Serialize(new NotificationEvent
                    {
                        EmployeeEmail = command.EmployeeEmail,
                        EmployeeName = command.EmployeeName,
                        ManagerEmail = command.ManagerEmail,
                        ManagerName = command.ManagerName,
                        Payload = new MerchDeliveryEventPayload
                        {
                            ClothingSize = (ClothingSize)command.ClothingSize,
                            MerchType = (MerchType)command.MerchPackType
                        }
                    })
                });
            
            return Task.FromResult(Unit.Value);
        }
    }
}