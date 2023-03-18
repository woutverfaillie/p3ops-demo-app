using Domain.Products;
using System;
using Xunit;

namespace Domain.Tests.Products
{
    public class ProductTest
    {
        private readonly Category _category;
        private readonly Product _product;

        public ProductTest()
        {
            _category = new CategoryFaker().Generate();
            _product = new Product("Test Product", "Test Description", 10, true, _category);
        }

        [Fact]
        public void NewProduct_NullCategory_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(
                "Test Product", "Test Description", 10, true, null
            ));
        }

        [Fact]
        public void NewProduct_NullPrice_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(
                "Test Product", "Test Description", null, true, _category
            ));
        }

        [Fact]
        public void NewProduct_NullName_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(
                null, "Test Description", 10, true, _category
            ));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void NewProduct_EmptyName_ThrowsException(string name)
        {
            Assert.Throws<ArgumentException>(() => new Product(
                name, "Test Description", 10, true, _category
            ));
        }

        [Fact]
        public void NewProduct_ValidAttributes_IsCreatedCorrectly()
        {
            Assert.Equal("Test Product", _product.Name);
            Assert.Equal("Test Description", _product.Description);
            Assert.Equal(10, _product.Price);
            Assert.True(_product.InStock);
            Assert.Equal(_category, _product.Category);
        }
    }
}
