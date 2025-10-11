using Domain.Common.Extensions;
using Domain.Common.Utilities;
using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggregate
{
    public class UserSession : AuditEntity, IExpirable
    {
        public string RefreshTokenHash { get; private set; } = default!;
        public string DeviceInfo { get; private set; } = default!;
        public string IPAddress { get; private set; } = default!;

        public DateTimeOffset ExpiresAt { get; set; }

        protected UserSession() { }

        private UserSession(string refreshTokenHash, string deviceInfo, string ipAddress)
        {
            RefreshTokenHash = refreshTokenHash;
            DeviceInfo = deviceInfo;
            IPAddress = ipAddress;
        }

        public static UserSession CreateNewSession(
            string deviceInfo = "",
            string ipAddress = "",
            TimeSpan? duration = null)
        {
            var refreshToken = HashHelper.GenerateSecureToken(64);
            var refreshTokenHash = HashHelper.HashToken(refreshToken);

            var session = new UserSession(refreshTokenHash, deviceInfo, ipAddress);
            session.MarkExpiredIn(duration ?? TimeSpan.FromDays(7));

            session._plainToken = refreshToken;
            return session;
        }
        public void RenewSession(TimeSpan? duration = null)
        {
            this.MarkExpiredIn(duration ?? TimeSpan.FromHours(1));
            this.MarkUpdated();
        }

        public bool ValidateRefreshToken(string token)
        {
            return RefreshTokenHash == HashHelper.HashToken(token) && !this.IsExpired();
        }

        // Lưu token gốc tạm thời (không map sang DB)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        private string? _plainToken;
        public string? GetPlainToken() => _plainToken;
    }
}
