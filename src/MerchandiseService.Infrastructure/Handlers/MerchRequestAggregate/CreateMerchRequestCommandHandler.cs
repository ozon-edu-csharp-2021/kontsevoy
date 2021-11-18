using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;
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
            var employeeId = new Id(request.EmployeeId);
            var employeeNotificationEmail = new Email(request.NotificationEmail);
            var employeeClothingSize = ClothingSize.GetById(request.ClothingSize);
            var merchPack = MerchPack.GetById(request.MerchPackType);

            var merchRequest = new MerchRequest(employeeId, employeeNotificationEmail, employeeClothingSize, merchPack, MerchRequestStatus.Created);

            merchRequest = await MerchRequestRepository.CreateAsync(merchRequest, cancellationToken);
            
            return merchRequest.Id.Value;
        }
    }
}