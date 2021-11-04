using System;
using System.Collections.Generic;
using System.Linq;

namespace MerchandiseService.Domain.Models
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode() => 
            GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);

        public ValueObject GetCopy() => MemberwiseClone() as ValueObject;
        
        public static bool operator ==(ValueObject left, ValueObject right) =>
            (left is null == right is null) && (left is null || left.Equals(right));

        public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);
    }

    public abstract class ValueObject<T> where T : IComparable
    {
        public T Value { get; }

        public ValueObject(T value) => Value = value;
        
        protected IEnumerable<object> GetEqualityComponents() 
        {
            yield return Value;
        }

        public override string ToString() => Value?.ToString();
    }
}