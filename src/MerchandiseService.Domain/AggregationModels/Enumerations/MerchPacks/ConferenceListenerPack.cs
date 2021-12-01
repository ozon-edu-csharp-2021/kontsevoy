using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class ConferenceListenerPack : MerchPack
    {
        protected internal ConferenceListenerPack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.ConferenceListenerPack, nameof(ConferenceListenerPack)) =>
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