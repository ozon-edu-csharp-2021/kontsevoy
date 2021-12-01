using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class ProbationPeriodEndingPack : MerchPack
    {
        protected internal ProbationPeriodEndingPack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.ProbationPeriodEndingPack, nameof(ProbationPeriodEndingPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.TShirt] = 1,
                    [MerchType.Sweatshirt] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}