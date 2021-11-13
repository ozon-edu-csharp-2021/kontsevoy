using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public class MerchKind : Enumeration
    {
        public static readonly MerchKind Accessory = new(1, nameof(Accessory));
        public static readonly MerchKind Clothing = new(2, nameof(Clothing));

        private MerchKind(int id, string name) : base(id, name) => Register(this);
        
        public static MerchKind GetById(int id) => Enumeration.GetById<MerchKind>(id);
    }
}