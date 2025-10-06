using Domain.SeedWork;
using Domain.Common.Extensions;
using Domain.Common.Exceptions;

namespace Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot, ICreationTrackable, IUpdateTrackable
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string UrlAvatar { get; private set; }
        public DateTimeOffset? EmailVerifiedAt { get; private set; }
        public bool IsDisabled { get; private set; }

        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        private List<UserAuthProvider> _userAuthProviders = new();
        public IReadOnlyCollection<UserAuthProvider> UserAuthProviders => _userAuthProviders.AsReadOnly();

        public User(string name, string email, string? urlAvatar)
        {
            Name = name;
            Email = email;
            UrlAvatar = urlAvatar ?? string.Empty;
            IsDisabled = false;
            EmailVerifiedAt = null;
            this.MarkCreated();
        }
        public void Update(string name, string email, string? urlAvatar)
        {
            Name = name;
            Email = email;
            UrlAvatar = urlAvatar ?? string.Empty;
            this.MarkUpdated();
        }
        public void Disabled() => IsDisabled = true;
        public void Enabled() => IsDisabled = false;
        public void MarkEmailAsVerified()
        {
            if (EmailVerifiedAt != null)
                throw new DomainException($"Email {Email} has been verified.");
            EmailVerifiedAt = DateTimeOffset.UtcNow;
        }
    }
}