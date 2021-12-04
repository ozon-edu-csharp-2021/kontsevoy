using System;
using System.Collections.Generic;
using System.Linq;

namespace MerchandiseService.Domain.Base.Models
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj) =>
            GetType() == obj?.GetType() && GetEqualityComponents().SequenceEqual(((ValueObject)obj).GetEqualityComponents());

        public override int GetHashCode() => 
            GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);

        public ValueObject GetCopy() => MemberwiseClone() as ValueObject;
        
        public static bool operator ==(ValueObject left, ValueObject right) =>
            (left is null == right is null) && (left is null || left.Equals(right));

        public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);
    }

    public abstract class ValueObject<T> : ValueObject where T : IComparable
    {
        public T Value { get; }

        public ValueObject(T value) => Value = value;
        
        protected override IEnumerable<object> GetEqualityComponents() 
        {
            yield return Value;
        }

        public override string ToString() => Value?.ToString();
    }

    public abstract class ClassValueObject<T> : ValueObject<T> where T : class, IComparable
    {
        public ClassValueObject(T value) : base(value) {}
        
        public static implicit operator T(ClassValueObject<T> value) => value?.Value;
    }
}