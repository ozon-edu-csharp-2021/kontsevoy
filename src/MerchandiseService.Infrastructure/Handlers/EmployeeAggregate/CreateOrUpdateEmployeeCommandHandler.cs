using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.EmployeeAggregate;

namespace MerchandiseService.Infrastructure.Handlers.EmployeeAggregate
{
    public class CreateOrUpdateEmployeeCommandHandler : IRequestHandler<CreateOrUpdateEmployeeCommand>
    {
        private IEmployeeRepository EmployeeRepository { get; }
        
        public CreateOrUpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository) =>
            EmployeeRepository = employeeRepository;
        
        public async Task<Unit> Handle(
            CreateOrUpdateEmployeeCommand request, 
            CancellationToken cancellationToken)
        {
            var employeeId = new EmployeeId(request.EmployeeId);
            var notificationEmail = new Email(request.NotificationEmail);
            var closingSize = ClothingSize.GetById(request.ClothingSize);
            
            var employee = await EmployeeRepository.FindByIdAsync(employeeId, cancellationToken);
            if (employee is null)
            {
                employee = new(employeeId, notificationEmail, closingSize);
                await EmployeeRepository.CreateAsync(employee, cancellationToken);
            }
            else
            {
                employee.ChangeNotificationEmail(notificationEmail);
                employee.ChangeClothingSize(closingSize);
                await EmployeeRepository.UpdateAsync(employee, cancellationToken);
            }
            
            await EmployeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}