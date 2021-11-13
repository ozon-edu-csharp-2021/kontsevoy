using System;
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
        private static readonly Dictionary<int, MerchPack> Registered = new();

        public static MerchPack GetById(int id)
        {
            if (!Registered.ContainsKey(id))
                throw new ArgumentException($"Invalid {nameof(id)} for {nameof(MerchPack)}: value = {id}", nameof(id));
            return Registered[id];
        }
        
        public static readonly MerchPack Welcome = new WelcomePack();
        public static readonly MerchPack ConferenceListener = new ConferenceListenerPack();
        public static readonly MerchPack ConferenceSpeaker = new ConferenceSpeakerPack();
        public static readonly MerchPack ProbationPeriodEnding = new ProbationPeriodEndingPack();
        public static readonly MerchPack Veteran = new VeteranPack();

        public ReadOnlyCollection<MerchItem> Items { get; init; }

        protected MerchPack(int id, string name) : base(id, name) => Registered[id] = this;
    }
    
    public static class Extension
    {
        internal static ReadOnlyCollection<MerchItem> ToReadOnlyMerchItemCollection(this Dictionary<MerchType, int> typeToQuantityDictionary) =>
            typeToQuantityDictionary.Select(f => new MerchItem(f.Key, new PositiveQuantity(f.Value)))
                .ToList().AsReadOnly();
    }
}