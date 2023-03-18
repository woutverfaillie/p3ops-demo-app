using Domain.Customers;
using System;
using Xunit;

namespace Domain.Tests.Customers
{
    public class CustomerNameTest
    {
        private readonly CustomerName _customerName;

        public CustomerNameTest()
        {
            _customerName = new CustomerName("Test", "Customer");
        }

        [Theory]
        [InlineData(null, "Customer")]
        [InlineData("Test", null)]
        public void NewCustomerName_NullName_ThrowsException(string firstname, string lastname)
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerName(firstname, lastname));
        }

        [Theory]
        [InlineData("", "Customer")]
        [InlineData("Test", "")]
        [InlineData("   ", "Customer")]
        [InlineData("Test", "   ")]
        public void NewCustomerName_EmptyName_ThrowsException(string firstname, string lastname)
        {
            Assert.Throws<ArgumentException>(() => new CustomerName(firstname, lastname));
        }

        [Fact]
        public void NewCustomerName_ValidName_IsCreatedCorrectly()
        {
            Assert.Equal("Test", _customerName.Firstname);
            Assert.Equal("Customer", _customerName.Lastname);
        }

        [Fact]
        public void CustomerName_EqualNames_ShouldBeEqual()
        {
            var customerName2 = new CustomerName(_customerName.Firstname, _customerName.Lastname);
            Assert.Equal(_customerName, customerName2);
        }
    }
}
