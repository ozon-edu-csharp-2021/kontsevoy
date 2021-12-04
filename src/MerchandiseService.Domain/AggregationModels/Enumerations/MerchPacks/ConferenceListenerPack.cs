using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class ConferenceListenerPack : MerchPack
    {
        protected internal ConferenceListenerPack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.ConferenceListenerPack, nameof(ConferenceListenerPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.NotepadConferenceListener] = 1,
                    [MerchType.PenConferenceListener] = 1,
                    [MerchType.TShirtConferenceListener] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}