using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Domain.Base.Contracts
{
    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="TAggregationRoot">Объект сущности для управления</typeparam>
    /// <typeparam name="TAggregationRootId">Объект идентификатора сущности для управления</typeparam>
    public interface IRepository<TAggregationRoot, in TAggregationRootId>
    {
        /// <summary>
        /// Создать новую сущность
        /// </summary>
        /// <param name="itemToCreate">Объект для создания</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Созданная сущность</returns>
        Task<TAggregationRoot> CreateAsync(TAggregationRoot itemToCreate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновить существующую сущность
        /// </summary>
        /// <param name="itemToUpdate">Объект для создания</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Обновленная сущность сущность</returns>
        Task<TAggregationRoot> UpdateAsync(TAggregationRoot itemToUpdate, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущность</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект сущности, если сущности с указанным id нет, то null</returns>
        Task<TAggregationRoot> FindByIdAsync(TAggregationRootId id, CancellationToken cancellationToken = default);
    }
}