using System.Collections.Generic;
using MerchTypeEnum = CSharpCourse.Core.Lib.Enums.MerchType;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class WelcomePack : MerchPack
    {
        protected internal WelcomePack() : base((int)MerchTypeEnum.WelcomePack, nameof(WelcomePack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.Pen] = 1,
                    [MerchType.Notepad] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}