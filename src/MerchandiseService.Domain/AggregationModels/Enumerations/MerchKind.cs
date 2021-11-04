using System;
using System.Collections.Generic;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public class MerchKind : Enumeration
    {
        private static readonly Dictionary<int, MerchKind> Registered = new();

        public static MerchKind GetById(int id)
        {
            if (!Registered.ContainsKey(id))
                throw new ArgumentException($"Invalid {nameof(id)} for {nameof(MerchKind)}: value = {id}", nameof(id));
            return Registered[id];
        }
        
        public static readonly MerchKind Accessory = new(1, nameof(Accessory));
        public static readonly MerchKind Clothing = new(2, nameof(Clothing));

        private MerchKind(int id, string name) : base(id, name) => Registered[id] = this;
    }
}