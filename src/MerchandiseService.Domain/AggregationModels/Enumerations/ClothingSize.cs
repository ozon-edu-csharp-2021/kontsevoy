using MerchandiseService.Domain.Base.Models;

namespace MerchandiseService.Domain.AggregationModels.Enumerations
{
    public class ClothingSize : Enumeration
    {
        public static readonly ClothingSize XS = new((int)CSharpCourse.Core.Lib.Enums.ClothingSize.XS, nameof(XS));
        public static readonly ClothingSize S = new((int)CSharpCourse.Core.Lib.Enums.ClothingSize.S, nameof(S));
        public static readonly ClothingSize M = new((int)CSharpCourse.Core.Lib.Enums.ClothingSize.M, nameof(M));
        public static readonly ClothingSize L = new((int)CSharpCourse.Core.Lib.Enums.ClothingSize.L, nameof(L));
        public static readonly ClothingSize XL = new((int)CSharpCourse.Core.Lib.Enums.ClothingSize.XL, nameof(XL));
        public static readonly ClothingSize XXL = new((int)CSharpCourse.Core.Lib.Enums.ClothingSize.XXL, nameof(XXL));

        private ClothingSize(int id, string name) : base(id, name) {}
        
        public static ClothingSize GetById(int id) => Enumeration.GetById<ClothingSize>(id);
        
        public static ClothingSize GetByName(string name) => Enumeration.GetByName<ClothingSize>(name);
        
        public static implicit operator ClothingSize(int value) => GetById(value);
        public static implicit operator ClothingSize(string value) => GetByName(value);
    }
}