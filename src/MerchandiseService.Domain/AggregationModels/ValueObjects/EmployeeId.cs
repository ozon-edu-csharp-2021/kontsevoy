using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class EmployeeId : ValueObject<ulong>
    {
        public EmployeeId(ulong value) : base(value) {}
    }
}