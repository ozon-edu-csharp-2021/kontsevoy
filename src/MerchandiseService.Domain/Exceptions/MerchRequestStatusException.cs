using System;

namespace MerchandiseService.Domain.Exceptions
{
    public class MerchRequestStatusException : Exception
    {
        public MerchRequestStatusException(string message) : base(message) {}
    }
}