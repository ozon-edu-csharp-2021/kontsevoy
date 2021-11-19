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
        
        private static readonly Dictionary<Type, Dictionary<int, object>> RegisteredIds = new();
        private static readonly Dictionary<Type, Dictionary<string, object>> RegisteredNames = new();

        private static void RegisterIfNotExists<T>() where T : Enumeration
        {
            var type = typeof(T);
            if (RegisteredIds.ContainsKey(type)) return;
            
            lock (RegisteredIds)
            {
                if (RegisteredIds.ContainsKey(type)) return;
                
                var ids = new Dictionary<int, object>();
                var names = new Dictionary<string, object>();

                foreach (var obj in GetAll<T>())
                {
                    ids[obj.Id] = obj;
                    names[obj.Name.ToLowerInvariant()] = obj;
                }
                RegisteredNames[type] = names;
                RegisteredIds[type] = ids;
            }
        }
        
        protected static T GetById<T>(int id) where T : Enumeration
        {
            RegisterIfNotExists<T>();
            var type = typeof(T);
            if (!RegisteredIds[type].ContainsKey(id))
                throw new ArgumentException($"Invalid {nameof(id)} for {typeof(T)}: value = {id}", nameof(id));
            return (T)RegisteredIds[type][id];
        }
        
        protected static T GetByName<T>(string name) where T : Enumeration
        {
            RegisterIfNotExists<T>();
            var type = typeof(T);
            var name_lower = name?.ToLowerInvariant();
            if (name_lower == null || !RegisteredNames[type].ContainsKey(name_lower))
                throw new ArgumentException($"Invalid {nameof(name)} for {typeof(T)}: value = {name}", nameof(name));
            return (T)RegisteredNames[type][name_lower];
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