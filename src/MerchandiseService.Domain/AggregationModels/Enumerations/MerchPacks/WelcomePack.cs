using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class WelcomePack : MerchPack
    {
        protected internal WelcomePack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.WelcomePack, nameof(WelcomePack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.Pen] = 1,
                    [MerchType.Notepad] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}