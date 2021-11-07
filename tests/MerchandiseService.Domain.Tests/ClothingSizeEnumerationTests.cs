using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.Models;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class ClothingSizeEnumerationTests
    {
        [Fact]
        public void CantGetValueByZeroId()
        {
            Assert.Throws<ArgumentException>(() => ClothingSize.GetById(0));
        }
        
        [Fact]
        public void ReferenceEquality()
        {
            Assert.Same(ClothingSize.XS, ClothingSize.GetById(ClothingSize.XS.Id));
            Assert.Same(ClothingSize.S, ClothingSize.GetById(ClothingSize.S.Id));
            Assert.Same(ClothingSize.M, ClothingSize.GetById(ClothingSize.M.Id));
            Assert.Same(ClothingSize.L, ClothingSize.GetById(ClothingSize.L.Id));
            Assert.Same(ClothingSize.XL, ClothingSize.GetById(ClothingSize.XL.Id));
            Assert.Same(ClothingSize.XXL, ClothingSize.GetById(ClothingSize.XXL.Id));
        }
        
        [Fact]
        public void GetByIdWorks()
        {
            foreach (var value in Enumeration.GetAll<ClothingSize>())
                Assert.Same(value, ClothingSize.GetById(value.Id));
        }
    }
}