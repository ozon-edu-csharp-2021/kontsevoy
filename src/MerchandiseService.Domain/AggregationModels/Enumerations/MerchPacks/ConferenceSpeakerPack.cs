using System.Collections.Generic;
using MerchTypeEnum = CSharpCourse.Core.Lib.Enums.MerchType;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class ConferenceSpeakerPack : MerchPack
    {
        protected internal ConferenceSpeakerPack() : base((int)MerchTypeEnum.ConferenceSpeakerPack, nameof(ConferenceSpeakerPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.TShirt] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}