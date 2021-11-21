using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;

namespace MerchandiseService.Infrastructure.Stubs
{
    public class MerchRequestRepositoryStub : EntityRepositoryStub<MerchRequest, Id>, IMerchRequestRepository
    {
        public MerchRequestRepositoryStub(IUnitOfWork unitOfWork) : base(unitOfWork) {}
        protected override Id GenerateId()
        {
            long max = 0;
            lock (Dictionary)
            {
                if (Dictionary.Count > 0)
                    max = Dictionary.Keys.Select(f => f.Value).Max();
            }

            return new Id(max + 1);
        }

        public Task<bool> ContainsByParamsAsync(Id employeeId, MerchPack merchPack, CancellationToken cancellationToken = default)
        {
            bool result;
            lock (Dictionary)
            {
                result = Dictionary.Values.Any(f => f.EmployeeId == employeeId && f.MerchPack == merchPack);
            }

            return Task.FromResult(result);
        }
    }
}