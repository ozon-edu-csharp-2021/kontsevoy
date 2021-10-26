using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;

namespace MerchandiseService.Services
{
    public class MerchandiseService : IMerchandiseService
    {
        public Task<long> CreateMerchRequest(MerchRequestCreationModel requestCreation, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> InquiryMerch(MerchInquiryModel requestInquiry, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}