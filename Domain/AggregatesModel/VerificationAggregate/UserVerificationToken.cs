using Domain.Common.Extensions;
using Domain.Common.Utilities;
using Domain.Events;
using Domain.SeedWork;

namespace Domain.AggregatesModel.VerificationAggregate
{
    public class UserVerificationToken : Entity, IExpirable, ICreationTrackable, IAggregateRoot
    {
        public long UserId { get; private set; }
        public string Token { get; private set; } = default!;
        public TypeOfVerificationToken Type { get; private set; }

        public DateTimeOffset? UsedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        protected UserVerificationToken() { }
        private UserVerificationToken(long userId, TypeOfVerificationToken type)
        {
            UserId = userId;
            Type = type;
            this.MarkCreated();
        }
        public static UserVerificationToken GenerateToken(long userId, TypeOfVerificationToken type, TimeSpan? duration = null)
        {
            var token = new UserVerificationToken(userId, type)
            {
                Token = HashHelper.GenerateSecureToken(32)
            };
            token.MarkExpiredIn(duration ?? TimeSpan.FromMinutes(5));
            token.AddDomainEvent(new SendEmailVerificationToken(token));
            return token;
        }
        public bool verifyToken(string token)
        {
            return Token == token && !this.IsExpired() && !UsedAt.HasValue;
        }
        public bool IsValid()
        {
            return DateTimeOffset.UtcNow < ExpiresAt && !UsedAt.HasValue;
        }
        public void MarkAsUsed()
        {
            if (UsedAt.HasValue)
                throw new InvalidOperationException("Token has already been used.");
            if (this.IsExpired())
                throw new InvalidOperationException("Token has expired and cannot be used.");

            UsedAt = DateTimeOffset.UtcNow;
        }
    }
}