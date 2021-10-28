using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;

namespace MerchandiseService.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task<long> CreateMerchRequest(MerchRequestCreationModel requestCreation, CancellationToken token);
        Task<bool> InquiryMerch(MerchInquiryModel requestInquiry, CancellationToken token);
    }
}