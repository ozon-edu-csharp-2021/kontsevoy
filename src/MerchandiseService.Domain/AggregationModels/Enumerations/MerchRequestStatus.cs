using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public class MerchRequestStatus : Enumeration
    {
        public static readonly MerchRequestStatus New = new(1, nameof(New));
        public static readonly MerchRequestStatus Processing = new(2, nameof(Processing));
        public static readonly MerchRequestStatus Done = new(3, nameof(Done));
        public static readonly MerchRequestStatus Decline = new(4, nameof(Decline));
        public static readonly MerchRequestStatus Awaiting = new(5, nameof(Awaiting));

        private MerchRequestStatus(int id, string name) : base(id, name) {}
        
        public static MerchRequestStatus GetById(int id) => Enumeration.GetById<MerchRequestStatus>(id);
        
        public static MerchRequestStatus GetByName(string name) => Enumeration.GetByName<MerchRequestStatus>(name);
        
        public static implicit operator MerchRequestStatus(int value) => GetById(value);
        public static implicit operator MerchRequestStatus(string value) => GetByName(value);
    }
}