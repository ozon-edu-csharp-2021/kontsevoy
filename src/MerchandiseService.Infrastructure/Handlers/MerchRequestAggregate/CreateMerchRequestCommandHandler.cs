using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.MerchRequestAggregate;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class CreateMerchRequestCommandHandler : IRequestHandler<CreateMerchRequestCommand, long>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }

        public CreateMerchRequestCommandHandler(IMerchRequestRepository merchRequestRepository, IMediator mediator) =>
            MerchRequestRepository = merchRequestRepository;
        
        public async Task<long> Handle(CreateMerchRequestCommand request, CancellationToken cancellationToken)
        {
            var employeeEmail = (Email)request.EmployeeEmail;
            var employeeName = (Name)request.EmployeeName;
            var managerEmail = (Email)request.ManagerEmail;
            var managerName = (Name)request.ManagerName;
            var employeeClothingSize = (ClothingSize)request.ClothingSize;
            var merchPack = (MerchPack)request.MerchPackType;

            var merchRequest = MerchRequest.New(CreationTimestamp.Now, employeeEmail, employeeName,
                managerEmail, managerName, employeeClothingSize, merchPack);

            merchRequest = await MerchRequestRepository.CreateAsync(merchRequest, cancellationToken);
            
            return merchRequest.Id.Value;
        }
    }
}