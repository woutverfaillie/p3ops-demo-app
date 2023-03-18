using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Domain.Common
{
    public class Address : ValueObject
    {
        public string Country { get; }
        public string City { get; }
        public string Postalcode { get; }
        public string Street { get; }

        private Address() { }
        public Address(string country, string city, string postalcode, string street)
        {
            Country = Guard.Against.NullOrWhiteSpace(country, nameof(country));
            City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
            Postalcode = Guard.Against.NullOrWhiteSpace(postalcode, nameof(postalcode));
            Street = Guard.Against.NullOrWhiteSpace(street, nameof(street));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street.ToLower();
            yield return Country.ToLower();
            yield return Postalcode.ToLower();
            yield return City.ToLower();
        }
    }
}
