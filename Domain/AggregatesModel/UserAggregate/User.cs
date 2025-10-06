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

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        private List<UserAuthProvider> _userAuthProviders;
        public IReadOnlyCollection<UserAuthProvider> UserAuthProviders => _userAuthProviders.AsReadOnly();

        protected User()
        {
            Name = string.Empty;
            Email = string.Empty;
            UrlAvatar = string.Empty;
            IsDisabled = false;
            _userAuthProviders = new List<UserAuthProvider>();
        }
        private User(string name, string email, string? urlAvatar)
        {
            UserValidator.EnsureValid(name, urlAvatar);
            if (!email.IsValidEmail())
                throw new DomainException("Invalid email format.");

            _userAuthProviders = new List<UserAuthProvider>();

            Name = name;
            Email = email;
            UrlAvatar = urlAvatar ?? string.Empty;
            IsDisabled = false;
            EmailVerifiedAt = null;

            this.MarkCreated();
            this.MarkUpdated();
        }
        public static User CreateLocalAccount(string name, string email, string passwordHash, string passwordSalt)
        {
            var user = new User(name, email, null);
            user.AddAuthProvider(AuthProvider.Local, passwordHash, passwordSalt);
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
        public void AddAuthProvider(AuthProvider provider, string? passwordHash = null, string? passwordSalt = null)
        {
            if (_userAuthProviders.Any(x => x.Provider == provider))
                throw new DomainException($"Auth provider {provider} already exists for user {Email}.");

            var userAuthProvider = new UserAuthProvider(provider);
            _userAuthProviders.Add(userAuthProvider);

            if (passwordHash != null && passwordSalt != null)
            {
                userAuthProvider.SetPassword(passwordHash, passwordSalt);
            }
        }
    }
}