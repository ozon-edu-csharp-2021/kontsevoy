using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;

namespace MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="MerchRequest"/>
    /// </summary>
    public interface IMerchRequestRepository : IRepository<MerchRequest, Id>
    {
        /// <summary>
        /// Проверить наличие сущности с заданными атрибутами
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="merchPack">Идентификатор комплекта</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>true, если нашёлся запрос на комплект для заданного сотрудника, иначе false</returns>
        Task<bool> ContainsByParamsAsync(Id employeeId, MerchPack merchPack, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить все сущности по статусу
        /// </summary>
        /// <param name="status">Статус запроса. <see cref="MerchRequestStatus"/></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Коллекция сущностей в указанном статусе</returns>
        Task<IReadOnlyCollection<MerchRequest>> FindByStatus(MerchRequestStatus status, CancellationToken cancellationToken = default);
    }
}