using System.Text.RegularExpressions;
using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggregate
{
    public readonly partial record struct Email
    {
        public string Address { get; init; } = default!;

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Email address cannot be empty.", nameof(address));

            if (!EmailRegex().IsMatch(address))
                throw new ArgumentException("Invalid email address format.", nameof(address));

            Address = address.Trim();
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
        private static partial Regex EmailRegex();
    }
}
