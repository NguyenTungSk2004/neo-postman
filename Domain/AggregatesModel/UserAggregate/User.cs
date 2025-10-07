using Domain.SeedWork;
using Domain.Common.Extensions;
using Domain.Common.Exceptions;

namespace Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot, ICreationTrackable, IUpdateTrackable
    {
        public string Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public string UrlAvatar { get; private set; } = default!;
        public DateTimeOffset? EmailVerifiedAt { get; private set; }
        public bool IsDisabled { get; private set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public UserVerificationToken? CurrentVerificationToken { get; set; }
        public List<UserSession> UserSessions { get; private set; } = new List<UserSession>();
        
        private List<UserAuthProvider> _userAuthProviders;
        public IReadOnlyCollection<UserAuthProvider> UserAuthProviders => _userAuthProviders.AsReadOnly();

        protected User()
        {
            _userAuthProviders = new List<UserAuthProvider>();
        }
        private User(string name, string email, string? urlAvatar)
        {
            UserValidator.EnsureValid(name, urlAvatar);

            _userAuthProviders = new List<UserAuthProvider>();

            Name = name;
            Email = new Email(email);
            UrlAvatar = urlAvatar ?? string.Empty;
            IsDisabled = false;
            EmailVerifiedAt = null;

            this.MarkCreated();
            this.MarkUpdated();
        }
        public static User CreateLocalAccount(string name, string email, string passwordHash)
        {
            var user = new User(name, email, null);
            user.AddAuthProvider(AuthProvider.Local, passwordHash);
            return user;
        }

        public void Update(string name, string? urlAvatar)
        {
            UserValidator.EnsureValid(name, urlAvatar);

            Name = name;
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
        public void AddAuthProvider(AuthProvider provider, string? passwordHash = null)
        {
            if (_userAuthProviders.Any(x => x.Provider == provider))
                throw new DomainException($"Auth provider {provider} already exists for user {Email}.");

            var userAuthProvider = new UserAuthProvider(provider);
            _userAuthProviders.Add(userAuthProvider);

            if (passwordHash != null)
            {
                userAuthProvider.SetPassword(passwordHash);
            }
        }
    }
}