using Domain.Common;
using System;
using Xunit;

namespace Domain.Tests.Common
{
    public class AddressTest
    {
        [Theory]
        [InlineData(null, "Postalcode", "City", "Country")]
        [InlineData("Street", null, "City", "Country")]
        [InlineData("Street", "Postalcode", null, "Country")]
        [InlineData("Street", "Postalcode", "City", null)]
        public void NewAddress_AttributeNull_ThrowsException(string street, string postalcode, string city, string country)
        {
            Assert.Throws<ArgumentNullException>(() => new Address(country, city, postalcode, street));
        }

        [Theory]
        [InlineData("", "Postalcode", "City", "Country")]
        [InlineData("  ", "Postalcode", "City", "Country")]
        [InlineData("Street", "", "City", "Country")]
        [InlineData("Street", "  ", "City", "Country")]
        [InlineData("Street", "Postalcode", "", "Country")]
        [InlineData("Street", "Postalcode", "  ", "Country")]
        [InlineData("Street", "Postalcode", "City", "")]
        [InlineData("Street", "Postalcode", "City", "  ")]
        public void NewAddress_AttributeEmpty_ThrowsException(string street, string postalcode, string city, string country)
        {
            Assert.Throws<ArgumentException>(() => new Address(country, city, postalcode, street));
        }

        [Fact]
        public void NewAddress_IsCreatedCorrectly()
        {
            var address = new Address("Country", "City", "Postalcode", "Street");

            Assert.Equal("Country", address.Country);
            Assert.Equal("City", address.City);
            Assert.Equal("Postalcode", address.Postalcode);
            Assert.Equal("Street", address.Street);
        }

        [Fact]
        public void NewAddress_WithSameValues_IsEqual()
        {
            var address1 = new Address("Country", "City", "Postalcode", "Street");
            var address2 = new Address(address1.Country, address1.City, address1.Postalcode, address1.Street);

            Assert.Equal(address1, address2);
        }
    }
}
