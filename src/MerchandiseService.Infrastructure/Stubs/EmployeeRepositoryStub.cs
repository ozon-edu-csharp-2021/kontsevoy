using System;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;

namespace MerchandiseService.Infrastructure.Stubs
{
    public class EmployeeRepositoryStub : EntityRepositoryStub<Employee, EmployeeId>, IEmployeeRepository
    {
        public EmployeeRepositoryStub(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        protected override EmployeeId GenerateId() =>
            throw new InvalidOperationException($"Generating {nameof(EmployeeId)} are not allow");
    }
}