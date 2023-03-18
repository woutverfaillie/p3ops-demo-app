using Domain.Orders;
using Domain.Products;
using System;
using Xunit;

namespace Domain.Tests.Orders
{
    public class CartLineTest
    {
        private readonly Product _product;
        private readonly CartLine _cartLine;

        public CartLineTest()
        {
            _product = new ProductFaker().Generate();
            _cartLine = new CartLine(_product, 2);
        }

        [Fact]
        public void NewCartLine_NegativeQuantity_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new CartLine(_product, -1));
        }

        [Fact]
        public void NewCartLine_ProductNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new CartLine(null, 1));
        }

        [Fact]
        public void NewCartLine_IsCreatedCorrectly()
        {
            Assert.Equal(_product, _cartLine.Product);
            Assert.Equal(2, _cartLine.Quantity);
        }

        [Fact]
        public void IncreaseQuantity_ShouldAddQuantity()
        {
            _cartLine.IncreaseQuantity(2);
            Assert.Equal(4, _cartLine.Quantity);
        }

        [Fact]
        public void CartLine_Total_ShouldReturnQuantityTimesPrice()
        {
            decimal expected = _product.Price * _cartLine.Quantity;
            Assert.Equal(expected, (decimal)_cartLine.Total);
        }
    }
}
