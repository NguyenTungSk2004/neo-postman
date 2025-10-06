using Domain.Common.Exceptions;
using Domain.Common.Extensions;
using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggregate
{
    public class UserAuthProvider : Entity, ICreationTrackable, IUpdateTrackable
    {
        public AuthProvider Provider { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? PasswordSalt { get; private set; }

        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public UserAuthProvider(AuthProvider provider)
        {
            Provider = provider;
            this.MarkCreated();
        }
        
        public void SetPassword(string passwordHash, string passwordSalt)
        {
            if (Provider != AuthProvider.Local)
                throw new DomainException("Cannot set password for non-local provider.");
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            this.MarkUpdated();
        }
    }
}