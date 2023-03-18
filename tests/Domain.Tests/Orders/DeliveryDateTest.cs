using Domain.Orders;
using System;
using Xunit;

namespace Domain.Tests.Orders
{
    public class DeliveryDateTest
    {
        private readonly DeliveryDate _deliveryDate;

        public DeliveryDateTest()
        {
            _deliveryDate = new DeliveryDate(DateTime.Now.AddDays(2));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(150)]
        public void NewDeliveryDate_OutOfRange_ThrowsException(int daysToAdd)
        {
            DateTime date = DateTime.Now.AddDays(daysToAdd);
            Assert.Throws<ArgumentOutOfRangeException>(() => new DeliveryDate(date));
        }

        [Fact]
        public void NewDeliveryDate_InRange_IsCreatedCorrectly()
        {
            DateTime date = DateTime.Now.AddDays(2);
            Assert.Equal(date.Date, _deliveryDate.Date);
        }

        [Fact]
        public void DeliveryDate_SameDate_ShouldBeEqual()
        {
            DeliveryDate deliveryDate = new DeliveryDate(_deliveryDate.Date);
            Assert.True(_deliveryDate.Equals(deliveryDate));
        }
    }
}
