using Domain.Common.Exceptions;
using Domain.Common.Extensions;

namespace Domain.AggregatesModel.UserAggregate
{
    public static class UserValidator
    {
        public static void EnsureValid(string name, string email, string? urlAvatar)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("User name is required.");

            if (!email.IsValidEmail())
                throw new DomainException("Invalid email format.");

            if (!string.IsNullOrWhiteSpace(urlAvatar) && 
                !Uri.IsWellFormedUriString(urlAvatar, UriKind.Absolute))
                throw new DomainException("Invalid avatar URL.");
        }
    }
}
