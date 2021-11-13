using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.EmployeeAggregate;
using MerchandiseService.Infrastructure.Commands.MerchRequestAggregate;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class CreateMerchRequestCommandHandler : IRequestHandler<CreateMerchRequestCommand, long>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }
        private IMediator Mediator { get; }

        public CreateMerchRequestCommandHandler(IMerchRequestRepository merchRequestRepository, IMediator mediator) =>
            (MerchRequestRepository, Mediator) = (merchRequestRepository, mediator);
        
        public async Task<long> Handle(CreateMerchRequestCommand request, CancellationToken cancellationToken)
        {
            var employeeId = new EmployeeId(request.EmployeeId);

            var saveEmployee = new CreateOrUpdateEmployeeCommand
            {
                EmployeeId = request.EmployeeId,
                NotificationEmail = request.NotificationEmail,
                ClothingSize = request.ClothingSize
            };

            await Mediator.Send(saveEmployee, cancellationToken);
            var merchPack = MerchPack.GetById(request.MerchPackType);

            var merchRequest = new MerchRequest(employeeId, merchPack);

            merchRequest = await MerchRequestRepository.CreateAsync(merchRequest, cancellationToken);
            
            await MerchRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return merchRequest.Id.Value;
        }
    }
}