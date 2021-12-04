using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace MerchandiseService.Infrastructure.Stubs
{
    public class MerchRequestRepositoryStub : EntityRepositoryStub<MerchRequest, Id>, IMerchRequestRepository
    {
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

        public Task<IReadOnlyCollection<MerchRequest>> FindByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken = default)
        {
            lock (Dictionary)
            {
                return Task.FromResult<IReadOnlyCollection<MerchRequest>>(
                    Dictionary.Values.Where(f => f.EmployeeEmail == employeeEmail)
                        .OrderByDescending(f => f.CreatedAt).ToList().AsReadOnly());
            }
        }

        public Task<IReadOnlyCollection<MerchRequest>> FindByStatus(MerchRequestStatus status, CancellationToken cancellationToken = default)
        {
            lock (Dictionary)
            {
                return Task.FromResult<IReadOnlyCollection<MerchRequest>>(
                    Dictionary.Values.Where(f => f.Status == status).ToList().AsReadOnly());
            }
        }
    }
}