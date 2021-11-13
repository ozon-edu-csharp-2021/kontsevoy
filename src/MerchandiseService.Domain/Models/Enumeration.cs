using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MerchandiseService.Domain.Models
{
    public abstract class Enumeration : IComparable
    {
        protected internal Enumeration(int id, string name) => (Id, Name) = (id, name);
        
        public int Id { get; }
        public string Name { get; }

        public override string ToString() => Name;
        
        private static readonly Dictionary<Type, Dictionary<int, object>> Registered = new();

        protected static void Register<T>(T obj) where T : Enumeration
        {
            var type = typeof(T);
            if (!Registered.ContainsKey(type))
                Registered[type] = new Dictionary<int, object>();
            Registered[type][obj.Id] = obj;
        }
        
        protected static T GetById<T>(int id) where T : Enumeration
        {
            var type = typeof(T);
            if (!Registered.ContainsKey(type) || !Registered[type].ContainsKey(id))
                throw new ArgumentException($"Invalid {nameof(id)} for {typeof(T)}: value = {id}", nameof(id));
            return (T)Registered[type][id];
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public override bool Equals(object obj) 
            => GetType() == obj?.GetType() && Id.Equals(((Enumeration)obj).Id);

        public override int GetHashCode() => Id.GetHashCode();

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
        
        public static bool operator ==(Enumeration left, Enumeration right) =>
            (left is null == right is null) && (left is null || left.Equals(right));

        public static bool operator !=(Enumeration left, Enumeration right) => !(left == right);
    }
}