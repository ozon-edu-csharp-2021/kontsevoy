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
    }
}