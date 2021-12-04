using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Base.Contracts;

namespace MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="MerchRequest"/>
    /// </summary>
    public interface IMerchRequestRepository : IRepository<MerchRequest, Id>
    {
        /// <summary>
        /// Получить все сущности с подходящим email
        /// </summary>
        /// <param name="employeeEmail">Email сотрудника</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Коллекция сущностей с подходящим email</returns>
        Task<IReadOnlyCollection<MerchRequest>> FindByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить все сущности по статусу
        /// </summary>
        /// <param name="status">Статус запроса. <see cref="MerchRequestStatus"/></param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Коллекция сущностей в указанном статусе</returns>
        Task<IReadOnlyCollection<MerchRequest>> FindByStatus(MerchRequestStatus status, CancellationToken cancellationToken = default);
    }
}