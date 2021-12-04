using System.Collections.Generic;

namespace MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks
{
    public class WelcomePack : MerchPack
    {
        protected internal WelcomePack() : base(
            (int)CSharpCourse.Core.Lib.Enums.MerchType.WelcomePack, nameof(WelcomePack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.NotepadStarter] = 1,
                    [MerchType.PenStarter] = 1,
                    [MerchType.SocksStarter] = 1,
                    [MerchType.TShirtStarter] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
}