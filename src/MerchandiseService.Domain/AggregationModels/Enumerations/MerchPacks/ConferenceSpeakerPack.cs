using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class ConferenceSpeakerPack : MerchPack
    {
        protected internal ConferenceSpeakerPack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.ConferenceSpeakerPack, nameof(ConferenceSpeakerPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.NotepadConferenceSpeaker] = 1,
                    [MerchType.PenConferenceSpeaker] = 1,
                    [MerchType.SweatshirtConferenceSpeaker] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}