namespace MerchandiseService.Infrastructure.Kafka.Configuration
{
    /// <summary>
    /// Модель конфигураций для подключения к кафке
    /// </summary>
    public class KafkaConfiguration
    {
        /// <summary>
        /// Адрес сервера кафки 
        /// </summary>
        public string BootstrapServers { get; set; }
        
        /// <summary>
        /// Топик для событий о пополнении склада
        /// </summary>
        public string ReplenishedTopic { get; set; }
        
        /// <summary>
        /// Идентификатор ConsumerGroup для событий о пополнении склада
        /// </summary>
        public string ReplenishedGroup { get; set; }
        
        /// <summary>
        /// Топик для создания событий уведомления по email
        /// </summary>
        public string EmailNotificationTopic { get; set; }
        
        /// <summary>
        /// Топик для событий о сотрудниках
        /// </summary>
        public string EmployeeNotificationTopic { get; set; }
        
        /// <summary>
        /// Идентификатор ConsumerGroup для событий о сотрудниках
        /// </summary>
        public string EmployeeNotificationGroup { get; set; }
    }
}