using Domain.Products;
using System;
using Xunit;

namespace Domain.Tests.Products
{
    public class CategoryTest
    {
        private readonly Category _category;

        public CategoryTest()
        {
            _category = new Category("Test Category");
        }

        [Fact]
        public void NewCategory_NullName_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Category(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void NewCategory_EmptyName_ThrowsException(string name)
        {
            Assert.Throws<ArgumentException>(() => new Category(name));
        }

        [Fact]
        public void NewCategory_ValidName_IsCreatedCorrectly()
        {
            Assert.Equal("Test Category", _category.Name);
        }
    }
}
