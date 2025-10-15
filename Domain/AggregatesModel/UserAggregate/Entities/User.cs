using Domain.SeedWork;
using Domain.Common.Extensions;
using Domain.Common.Exceptions;
using Domain.Events;
using Domain.AggregatesModel.UserAggregate.ValueObjects;
using Domain.AggregatesModel.UserAggregate.Enums;
using Domain.AggregatesModel.UserAggregate.Validators;

namespace Domain.AggregatesModel.UserAggregate.Entities
{
    public class User : AuditEntity, IAggregateRoot
    {
        public string Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public string UrlAvatar { get; private set; } = default!;
        public DateTimeOffset? EmailVerifiedAt { get; private set; }
        public bool IsDisabled { get; private set; }

        private List<UserVerificationToken> _currentVerificationToken;
        public IReadOnlyCollection<UserVerificationToken> CurrentVerificationToken => _currentVerificationToken.AsReadOnly();

        private List<UserSession> _userSessions;
        public IReadOnlyCollection<UserSession> UserSessions => _userSessions.AsReadOnly();
        
        private List<UserAuthProvider> _userAuthProviders;
        public IReadOnlyCollection<UserAuthProvider> UserAuthProviders => _userAuthProviders.AsReadOnly();

        protected User()
        {
            _userSessions = new List<UserSession>();
            _userAuthProviders = new List<UserAuthProvider>();
            _currentVerificationToken = new List<UserVerificationToken>();
        }
        private User(string name, string email, string? urlAvatar)
        {
            UserValidator.EnsureValid(name, urlAvatar);

            _userSessions = new List<UserSession>();
            _userAuthProviders = new List<UserAuthProvider>();
            _currentVerificationToken = new List<UserVerificationToken>();

            Name = name;
            Email = new Email(email);
            UrlAvatar = urlAvatar ?? string.Empty;
            IsDisabled = false;
            EmailVerifiedAt = null;
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
        public void ChangePassword(string newPasswordHash)
        {
            var localProvider = _userAuthProviders.FirstOrDefault(x => x.Provider == AuthProvider.Local);
            if (localProvider is null)
                throw new DomainException("Local auth provider does not exist.");

            localProvider.SetPassword(newPasswordHash);
        }
        public void AddSession(string userAgent, string ipAddress)
        {
            var userSession = UserSession.CreateNewSession(userAgent, ipAddress);
            _userSessions.Add(userSession);
        }
        public void AddToken(TypeOfVerificationToken type, TimeSpan? duration = null)
        {
            var token = UserVerificationToken.GenerateToken(type, duration);
            token.AddDomainEvent(new SendEmailVerificationToken(token));
            _currentVerificationToken.Add(token);
        }
    }
}