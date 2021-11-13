using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;

namespace MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="Employee"/>
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee, EmployeeId> {}
}