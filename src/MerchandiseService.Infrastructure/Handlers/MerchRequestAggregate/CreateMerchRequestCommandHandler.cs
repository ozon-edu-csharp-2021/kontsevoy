using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.MerchRequestAggregate;
using OpenTracing;

namespace MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class CreateMerchRequestCommandHandler : IRequestHandler<CreateMerchRequestCommand, long>
    {
        private IMerchRequestRepository MerchRequestRepository { get; }
        private ITracer Tracer { get; }

        public CreateMerchRequestCommandHandler(IMerchRequestRepository merchRequestRepository, ITracer tracer) =>
            (MerchRequestRepository, Tracer) = (merchRequestRepository, tracer);
        
        public async Task<long> Handle(CreateMerchRequestCommand request, CancellationToken cancellationToken)
        {
            using var span = Tracer.BuildSpan(nameof(CreateMerchRequestCommandHandler)).StartActive();
            
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