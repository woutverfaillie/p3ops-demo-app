using Domain.Common;
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using System;
using Xunit;

namespace Domain.Tests.Customers
{
    public class CustomerTest
    {
        private readonly Address _address;
        private readonly CustomerName _customerName;
        private readonly Customer _customer;

        public CustomerTest()
        {
            _address = new AddressFaker().Generate();
            _customerName = new CustomerName("Test", "Customer");
            _customer = new Customer(_customerName, _address);
        }

        [Fact]
        public void NewCustomer_NameNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Customer(null, _address));
        }

        [Fact]
        public void NewCustomer_AddressNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Customer(_customerName, null));
        }

        [Fact]
        public void NewCustomer_IsCreatedCorrectly()
        {
            Assert.Equal(_customerName, _customer.Name);
            Assert.Equal(_address, _customer.Address);
            Assert.Empty(_customer.Orders);
        }

        [Fact]
        public void PlaceOrder_ShouldAddOrderToCustomer()
        {
            var deliveryDate = new DeliveryDate(DateTime.Now.AddDays(2));
            var shippingAddress = new AddressFaker().Generate();
            var cart = new Cart();
            cart.AddItem(new ProductFaker().Generate(), 1);

            var order = _customer.PlaceOrder(cart, deliveryDate, false, shippingAddress);

            Assert.Single(_customer.Orders);
            Assert.Equal(order, _customer.Orders[0]);
        }
    }
}
