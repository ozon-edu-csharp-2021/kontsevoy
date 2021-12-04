using System.Diagnostics.CodeAnalysis;
using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public class MerchType : Enumeration
    {
        public static readonly MerchType TShirtStarter = new(1, nameof(TShirtStarter), MerchKind.Clothing);
        public static readonly MerchType NotepadStarter = new(2, nameof(NotepadStarter), MerchKind.Accessory);
        public static readonly MerchType PenStarter = new(3, nameof(PenStarter), MerchKind.Accessory);
        public static readonly MerchType SocksStarter = new(4, nameof(SocksStarter), MerchKind.Clothing);
        public static readonly MerchType TShirtAfterProbation = new(5, nameof(TShirtAfterProbation), MerchKind.Clothing);
        public static readonly MerchType SweatshirtAfterProbation = new(6, nameof(SweatshirtAfterProbation), MerchKind.Clothing);
        public static readonly MerchType SweatshirtConferenceSpeaker = new(7, nameof(SweatshirtConferenceSpeaker), MerchKind.Clothing);
        public static readonly MerchType NotepadConferenceSpeaker = new(8, nameof(NotepadConferenceSpeaker), MerchKind.Accessory);
        public static readonly MerchType PenConferenceSpeaker = new(9, nameof(PenConferenceSpeaker), MerchKind.Accessory);
        public static readonly MerchType TShirtConferenceListener = new(10, nameof(TShirtConferenceListener), MerchKind.Clothing);
        public static readonly MerchType NotepadConferenceListener = new(11, nameof(NotepadConferenceListener), MerchKind.Accessory);
        public static readonly MerchType PenConferenceListener = new(12, nameof(PenConferenceListener), MerchKind.Accessory);
        public static readonly MerchType TShirtVeteran = new(13, nameof(TShirtVeteran), MerchKind.Clothing);
        public static readonly MerchType SweatshirtVeteran = new(14, nameof(SweatshirtVeteran), MerchKind.Clothing);
        public static readonly MerchType NotepadVeteran = new(15, nameof(NotepadVeteran), MerchKind.Accessory);
        public static readonly MerchType PenVeteran = new(16, nameof(PenVeteran), MerchKind.Accessory);
        public static readonly MerchType CardHolderVeteran = new(17, nameof(CardHolderVeteran), MerchKind.Accessory);
        
        public MerchKind Kind { get; }

        private MerchType(int id, string name, [NotNull] MerchKind kind) : base(id, name) => Kind = kind;
        
        public static MerchType GetById(int id) => Enumeration.GetById<MerchType>(id);
        
        public static MerchType GetByName(string name) => Enumeration.GetByName<MerchType>(name);
        
        public static implicit operator MerchType(int value) => GetById(value);
        public static implicit operator MerchType(string value) => GetByName(value);
    }
}