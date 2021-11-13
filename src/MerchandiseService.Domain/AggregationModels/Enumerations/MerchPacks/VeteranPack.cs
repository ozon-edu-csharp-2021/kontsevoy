using System.Collections.Generic;
using MerchTypeEnum = CSharpCourse.Core.Lib.Enums.MerchType;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class VeteranPack : MerchPack
    {
        protected internal VeteranPack() : base((int)MerchTypeEnum.VeteranPack, nameof(VeteranPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.Socks] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}