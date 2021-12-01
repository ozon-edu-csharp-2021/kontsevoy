using System;
using System.Net.Mail;
using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class Email : ClassValueObject<string>
    {
        public Email(string value) : base(value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value), $"{nameof(value)} must be provided");
            if (!MailAddress.TryCreate(value, out var result))
                throw new ArgumentException($"{nameof(value)} must be valid e-mail address", nameof(value));
        }

        public static implicit operator Email(string value) => new(value);
        
        public static bool operator ==(Email left, Email right) =>
            (left is null == right is null) && (left is null || string.Equals(left.Value, right.Value, StringComparison.InvariantCultureIgnoreCase));

        public static bool operator !=(Email left, Email right) => !(left == right);
        
        public override bool Equals(object obj) =>
            GetType() == obj?.GetType() && this == obj as Email;
        
        public override int GetHashCode() => Value.GetHashCode();
    }
}