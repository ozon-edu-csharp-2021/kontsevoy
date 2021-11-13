using System;
using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class MerchItem : ValueObject
    {
        public MerchType MerchType { get; }
        
        public PositiveQuantity Quantity { get; }

        public MerchItem(MerchType type, PositiveQuantity quantity)   
        {
            MerchType = type ?? throw new ArgumentNullException(nameof(type), $"{nameof(type)} must be provided");
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity), 
                $"{nameof(quantity)} must be provided");
        }
        
        protected override IEnumerable<object> GetEqualityComponents() 
        {
            yield return MerchType;
            yield return Quantity;
        }
    }
}