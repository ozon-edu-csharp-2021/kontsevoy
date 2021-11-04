using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;
using MerchTypeEnum = CSharpCourse.Core.Lib.Enums.MerchType;

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

    public class ConferenceSpeakerPack : MerchPack
    {
        protected internal ConferenceSpeakerPack() : base((int)MerchTypeEnum.ConferenceSpeakerPack, nameof(ConferenceSpeakerPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.TShirt] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }

    public class ProbationPeriodEndingPack : MerchPack
    {
        protected internal ProbationPeriodEndingPack() : base((int)MerchTypeEnum.ProbationPeriodEndingPack, nameof(ProbationPeriodEndingPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.TShirt] = 1,
                    [MerchType.Sweatshirt] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }

    public class VeteranPack : MerchPack
    {
        protected internal VeteranPack() : base((int)MerchTypeEnum.VeteranPack, nameof(VeteranPack)) =>
            Items = new Dictionary<MerchType, int>
                {
                    [MerchType.Socks] = 1
                }
                .ToReadOnlyMerchItemCollection();
    }
    
    public static class Extension
    {
        internal static ReadOnlyCollection<MerchItem> ToReadOnlyMerchItemCollection(this Dictionary<MerchType, int> typeToQuantityDictionary) =>
            typeToQuantityDictionary.Select(f => new MerchItem(f.Key, new PositiveQuantity(f.Value)))
                .ToList().AsReadOnly();
    }
}