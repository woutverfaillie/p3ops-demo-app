using Domain.Products;
using Domain.Orders;
using System;
using Xunit;

namespace Domain.Tests.Orders
{
    public class CartTest
    {
        private readonly Cart _cart;
        private readonly Product _product;

        public CartTest()
        {
            _cart = new Cart();
            _product = new ProductFaker().Generate();
        }

        [Fact]
        public void NewCart_HasNoLines()
        {
            Assert.Empty(_cart.Lines);
            Assert.Equal(0, _cart.Total);
        }

        [Fact]
        public void AddItem_NonExistingProduct_AddsProduct()
        {
            var line = _cart.AddItem(_product, 1);

            Assert.Single(_cart.Lines);
            Assert.Equal(_product, line.Product);
            Assert.Equal(1, line.Quantity);
            Assert.Equal(_product.Price, line.Total);
        }

        [Fact]
        public void AddItem_ExistingProduct_IncreasesQuantity()
        {
            _cart.AddItem(_product, 1);
            var line = _cart.AddItem(_product, 1);

            Assert.Single(_cart.Lines);
            Assert.Equal(_product, line.Product);
            Assert.Equal(2, line.Quantity);
            Assert.Equal((decimal)_product.Price * 2, (decimal)line.Total);
        }

        [Fact]
        public void RemoveLine_ExistingProduct_RemovesProduct()
        {
            _cart.AddItem(_product, 1);
            _cart.RemoveLine(_product);

            Assert.Empty(_cart.Lines);
            Assert.Equal(0, _cart.Total);
        }

        [Fact]
        public void RemoveLine_NonExistingProduct_DoesNothing()
        {
            _cart.RemoveLine(_product);

            Assert.Empty(_cart.Lines);
            Assert.Equal(0, _cart.Total);
        }

        [Fact]
        public void Clear_EmptyCart_DoesNothing()
        {
            _cart.Clear();

            Assert.Empty(_cart.Lines);
            Assert.Equal(0, _cart.Total);
        }

        [Fact]
        public void Clear_NonEmptyCart_RemovesAllLines()
        {
            _cart.AddItem(_product, 1);
            _cart.Clear();

            Assert.Empty(_cart.Lines);
            Assert.Equal(0, _cart.Total);
        }
    }
}
