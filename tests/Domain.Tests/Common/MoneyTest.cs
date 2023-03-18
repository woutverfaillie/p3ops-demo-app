using Domain.Common;
using System;
using Xunit;

namespace Domain.Tests.Common
{
    public class MoneyTest
    {
        [Fact]
        public void NewMoney_IsCreatedCorrectly()
        {
            decimal value = 10m;
            Money money = value;

            Assert.Equal(value, money.Value);
        }

        [Theory]
        [InlineData(10, 10, 20)]
        [InlineData(0, 1234, 1234)]
        [InlineData(-12, 6, -6)]
        public void Money_Addition_ShouldBeCorrect(decimal value1, decimal value2, decimal expected)
        {
            Money money1 = value1;
            Money money2 = value2;

            Money result = money1 + money2;

            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData(20, 10, 10)]
        [InlineData(20, 20, 0)]
        [InlineData(500, 600, -100)]
        public void Money_Subtraction_ShouldBeCorrect(decimal value1, decimal value2, decimal expected)
        {
            Money money1 = value1;
            Money money2 = value2;

            Money result = money1 - money2;

            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData(4, 6, 24)]
        [InlineData(0, 10, 0)]
        [InlineData(-1, 2, -2)]
        public void Money_Multiplication_ShouldBeCorrect(decimal value1, decimal value2, decimal expected)
        {
            Money money1 = value1;
            Money money2 = value2;

            Money result = money1 * money2;

            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData(24, 6, 4)]
        [InlineData(0, 10, 0)]
        [InlineData(-2, -1, 2)]
        public void Money_Division_ShouldBeCorrect(decimal value1, decimal value2, decimal expected)
        {
            Money money1 = value1;
            Money money2 = value2;

            Money result = money1 / money2;

            Assert.Equal(expected, result.Value);
        }
    }
}
