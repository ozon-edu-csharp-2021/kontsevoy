using System;
using System.Net.Mail;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class Email : ClassValueObject<string>
    {
        public Email(string value) : base(value)
        {
            if (!MailAddress.TryCreate(value, out var result))
                throw new ArgumentException($"{nameof(value)} must be valid e-mail address", nameof(value));
        }

        public static implicit operator Email(string value) => new(value);
    }
}