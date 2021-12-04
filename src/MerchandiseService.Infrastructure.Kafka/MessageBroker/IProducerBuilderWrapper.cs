using Confluent.Kafka;

namespace MerchandiseService.Infrastructure.Kafka.MessageBroker
{
    public interface IProducerBuilderWrapper
    {
        /// <summary>
        /// Producer instance
        /// </summary>
        IProducer<string, string> Producer { get; }

        /// <summary>
        /// Топик для отправки сообщения о том что необходимо оповестить сотрудника, что пора забирать мерч
        /// </summary>
        string EmailNotificationTopic { get; }
    }
}