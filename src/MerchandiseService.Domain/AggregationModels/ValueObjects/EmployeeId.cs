using System;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class EmployeeId : ValueObject<long>
    {
        public EmployeeId(long value) : base(value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"{nameof(value)} of {nameof(EmployeeId)} must be greater than zero");
        }
    }
}