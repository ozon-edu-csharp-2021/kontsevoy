namespace MerchandiseService.Infrastructure.Configuration
{
    /// <summary>
    /// Модель конфигураций для подключения к Jaeger
    /// </summary>
    public class JaegerConfiguration
    {
        /// <summary>
        /// Адрес сервера Jaeger 
        /// </summary>
        public string ServerAddress { get; set; } = string.Empty;
    }
}