using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Domain.Customers
{
    public class CustomerName : ValueObject
    {
        public string Firstname { get; }
        public string Lastname { get; }

        private CustomerName() { }
        public CustomerName(string firstname, string lastname)
        {
            Firstname = Guard.Against.NullOrWhiteSpace(firstname, nameof(firstname));
            Lastname = Guard.Against.NullOrWhiteSpace(lastname, nameof(lastname));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Firstname.ToLower();
            yield return Lastname.ToLower();
        }
    }
}
