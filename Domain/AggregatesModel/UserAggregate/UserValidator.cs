using Domain.Common.Exceptions;

namespace Domain.AggregatesModel.UserAggregate
{
    public static class UserValidator
    {
        public static void EnsureValid(string name, string? urlAvatar)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("User name is required.");

            if (!string.IsNullOrWhiteSpace(urlAvatar) && 
                !Uri.IsWellFormedUriString(urlAvatar, UriKind.Absolute))
                throw new DomainException("Invalid avatar URL.");
        }
    }
}
