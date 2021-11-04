using MediatR;

namespace MerchandiseService.Infrastructure.Commands.EmployeeAggregate
{
    public class CreateOrUpdateEmployeeCommand : IRequest
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public ulong EmployeeId { get; init; }
        
        /// <summary>
        /// Адрес почты сотрудника для уведомлений
        /// </summary>
        public string NotificationEmail { get; init; }
        
        /// <summary>
        /// Идентификатор размера одежды 
        /// </summary>
        public int ClothingSize { get; init; }
    }
}