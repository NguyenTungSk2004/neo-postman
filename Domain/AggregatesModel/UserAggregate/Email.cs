using System.Text.RegularExpressions;
using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggregate
{
    public class Email : ValueObject
    {
        public string Address { get; private set; } = default!;

        private static readonly Regex EmailRegex =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private Email(){} 

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Email address cannot be empty.", nameof(address));

            if (!EmailRegex.IsMatch(address))
                throw new ArgumentException("Invalid email address format.", nameof(address));

            Address = address.Trim();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }

        public override string ToString() => Address;
    }
}
