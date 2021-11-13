using System.Collections.Generic;
using MerchTypeEnum = CSharpCourse.Core.Lib.Enums.MerchType;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class ConferenceListenerPack : MerchPack
    {
        protected internal ConferenceListenerPack() : base((int)MerchTypeEnum.ConferenceListenerPack, nameof(ConferenceListenerPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.Pen] = 1,
                    [MerchType.Notepad] = 1,
                    [MerchType.Sweatshirt] = 1,
                    [MerchType.Bag] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}