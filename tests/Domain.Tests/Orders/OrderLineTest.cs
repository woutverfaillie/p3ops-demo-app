using Domain.Products;
using Domain.Orders;
using System;
using Xunit;

namespace Domain.Tests.Orders
{
    public class OrderLineTest
    {
        [Fact]
        public void NewOrderLine_ProductNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderLine(null, 1));
        }

        [Fact]
        public void NewOrderLine_QuantityNegative_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new OrderLine(new ProductFaker().Generate(), -1));
        }

        [Fact]
        public void NewOrderLine_IsCreatedCorrectly()
        {
            var quantity = 1;
            var product = new ProductFaker().Generate();

            var orderLine = new OrderLine(product, quantity);

            Assert.Equal(product, orderLine.Product);
            Assert.Equal(quantity, orderLine.Quantity);
            Assert.Equal(product.Price, orderLine.Price);
        }
    }
}
