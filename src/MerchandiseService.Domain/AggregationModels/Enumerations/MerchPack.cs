using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MerchandiseService.Domain.AggregationModels.Enumerations.MerchPacks;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public abstract class MerchPack : Enumeration
    {
        public static readonly MerchPack Welcome = new WelcomePack();
        public static readonly MerchPack ConferenceListener = new ConferenceListenerPack();
        public static readonly MerchPack ConferenceSpeaker = new ConferenceSpeakerPack();
        public static readonly MerchPack ProbationPeriodEnding = new ProbationPeriodEndingPack();
        public static readonly MerchPack Veteran = new VeteranPack();

        public ReadOnlyCollection<MerchItem> Items { get; init; }

        protected MerchPack(int id, string name) : base(id, name) {}

        public static MerchPack GetById(int id) => Enumeration.GetById<MerchPack>(id);
        
        public static MerchPack GetByName(string name) => Enumeration.GetByName<MerchPack>(name);
        
        public static implicit operator MerchPack(int value) => GetById(value);
        public static implicit operator MerchPack(long value) => GetById((int)value);
        public static implicit operator MerchPack(string value) => value is null ? null : GetByName(value);
    }
    
    public static class Extension
    {
        internal static ReadOnlyCollection<MerchItem> ToReadOnlyMerchItemCollection(this Dictionary<MerchType, int> typeToQuantityDictionary) =>
            typeToQuantityDictionary.Select(f => new MerchItem(f.Key, new PositiveQuantity(f.Value)))
                .ToList().AsReadOnly();
    }
}