using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public class MerchRequestStatus : Enumeration
    {
        public static readonly MerchRequestStatus Created = new(1, nameof(Created));
        public static readonly MerchRequestStatus Process = new(2, nameof(Process));
        public static readonly MerchRequestStatus Done = new(3, nameof(Done));
        public static readonly MerchRequestStatus Decline = new(4, nameof(Decline));

        private MerchRequestStatus(int id, string name) : base(id, name) {}
        
        public static MerchRequestStatus GetById(int id) => Enumeration.GetById<MerchRequestStatus>(id);
        
        public static MerchRequestStatus GetByName(string name) => Enumeration.GetByName<MerchRequestStatus>(name);
    }
}