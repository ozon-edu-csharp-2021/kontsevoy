using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class VeteranPack : MerchPack
    {
        protected internal VeteranPack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.VeteranPack, nameof(VeteranPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.Socks] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}