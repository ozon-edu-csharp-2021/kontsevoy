using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class VeteranPack : MerchPack
    {
        protected internal VeteranPack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.VeteranPack, nameof(VeteranPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.NotepadVeteran] = 1,
                    [MerchType.PenVeteran] = 1,
                    [MerchType.CardHolderVeteran] = 1,
                    [MerchType.SweatshirtVeteran] = 1,
                    [MerchType.TShirtVeteran] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}