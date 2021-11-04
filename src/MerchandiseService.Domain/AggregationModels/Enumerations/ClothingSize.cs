using System;
using System.Collections.Generic;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public class ClothingSize : Enumeration
    {
        private static readonly Dictionary<int, ClothingSize> Registered = new();

        public static ClothingSize GetById(int id)
        {
            if (!Registered.ContainsKey(id))
                throw new ArgumentException($"Invalid {nameof(id)} for {nameof(ClothingSize)}: value = {id}", nameof(id));
            return Registered[id];
        }
        
        public static readonly ClothingSize XS = new(1, nameof(XS));
        public static readonly ClothingSize S = new(2, nameof(S));
        public static readonly ClothingSize M = new(3, nameof(M));
        public static readonly ClothingSize L = new(4, nameof(L));
        public static readonly ClothingSize XL = new(5, nameof(XL));
        public static readonly ClothingSize XXL = new(6, nameof(XXL));

        private ClothingSize(int id, string name) : base(id, name) => Registered[id] = this;
    }
}