using System;
using Confluent.Kafka;
using MerchandiseService.Infrastructure.Kafka.Configuration;
using Microsoft.Extensions.Options;

namespace MerchandiseService.Infrastructure.Kafka.MessageBroker
{
    public class ProducerBuilderWrapper : IProducerBuilderWrapper
    {
        /// <inheritdoc cref="Producer"/>
        public IProducer<string, string> Producer { get; }

        /// <inheritdoc cref="EmailNotificationTopic"/>
        public string EmailNotificationTopic { get; }

        public ProducerBuilderWrapper(IOptions<KafkaConfiguration> configuration)
        {
            var configValue = configuration.Value;
            if (configValue is null)
                throw new ApplicationException("Configuration for kafka server was not specified");

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configValue.BootstrapServers
            };

            Producer = new ProducerBuilder<string, string>(producerConfig).Build();
            EmailNotificationTopic = configValue.EmailNotificationTopic;
        }
    }
}