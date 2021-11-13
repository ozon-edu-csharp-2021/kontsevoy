using System.Collections.Generic;
using MerchTypeEnum = CSharpCourse.Core.Lib.Enums.MerchType;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class ProbationPeriodEndingPack : MerchPack
    {
        protected internal ProbationPeriodEndingPack() : base((int)MerchTypeEnum.ProbationPeriodEndingPack, nameof(ProbationPeriodEndingPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.TShirt] = 1,
                    [MerchType.Sweatshirt] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}