using Domain.Common.Exceptions;
using Domain.Common.Extensions;
using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggregate
{
    public class UserAuthProvider : AuditEntity
    {
        public AuthProvider Provider { get; private set; }
        public string? PasswordHash { get; private set; }

        public UserAuthProvider(AuthProvider provider)
        {
            Provider = provider;
        }
        
        public void SetPassword(string passwordHash)
        {
            if (Provider != AuthProvider.Local)
                throw new DomainException("Password can only be set for local auth provider.");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password hash must be provided for local auth provider.");
            PasswordHash = passwordHash;
            this.MarkUpdated();
        }
    }
}