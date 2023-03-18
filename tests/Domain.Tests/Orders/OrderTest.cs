using Domain.Common;
using Domain.Customers;
using Domain.Products;
using Domain.Orders;
using System.Collections.Generic;
using System;
using Xunit;

namespace Domain.Tests.Orders
{
    public class OrderTest
    {
        private readonly List<Product> _products;
        private readonly Cart _cart;
        private readonly DeliveryDate _deliveryDate;
        private readonly Address _address;
        private readonly Customer _customer;
        private readonly Order _order;

        public OrderTest()
        {
            _products = new ProductFaker().Generate(3);
            _cart = new Cart();

            foreach (var product in _products)
            {
                _cart.AddItem(product, 1);
            }

            _deliveryDate = new DeliveryDate(DateTime.Now.AddDays(5));
            _address = new AddressFaker().Generate();
            _customer = new CustomerFaker().Generate();
            _order = new Order(_cart, _deliveryDate, false, _customer, _address);

            // Add the products again because the cart is cleared when the order is created
            foreach (var product in _products)
            {
                _cart.AddItem(product, 1);
            }
        }

        [Fact]
        public void NewOrder_CartNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(null, _deliveryDate, false, _customer, _address));
        }

        [Fact]
        public void NewOrder_DeliveryDateNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(_cart, null, false, _customer, _address));
        }

        [Fact]
        public void NewOrder_CustomerNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(_cart, _deliveryDate, false, null, _address));
        }

        [Fact]
        public void NewOrder_AddressNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(_cart, _deliveryDate, false, _customer, null));
        }

        [Fact]
        public void NewOrder_IsCreatedCorrectly()
        {
            Assert.Equal(DateTime.UtcNow.Date, _order.OrderDate.Date);
            Assert.Equal(_deliveryDate, _order.DeliveryDate);
            Assert.False(_order.HasGiftWrapping);
            Assert.Equal(_address, _order.ShippingAddress);
            Assert.Equal(_customer, _order.Customer);
            Assert.Equal(_cart.Total, _order.Total);
            Assert.Equal(_cart.Lines.Count, _order.Items.Count);
            Assert.Equal(_cart.Total, _order.Total);
        }
    }
}
