using Ardalis.Specification;

namespace Domain.AggregatesModel.SessionAggregate.Specifications
{
    public class UserSessionSpecification : Specification<UserSession>
    {
        public UserSessionSpecification(string deviceInfo, string ipAddress)
        {
            Query.Where(s => s.DeviceInfo == deviceInfo && s.IPAddress == ipAddress && s.ExpiresAt > DateTimeOffset.UtcNow);
        }

        public static UserSessionSpecification ByRefreshTokenHash(string deviceInfo, string ipAddress, string refreshTokenHash)
        {
            var query = new UserSessionSpecification(deviceInfo, ipAddress);
            query.Query.Where(s => s.RefreshTokenHash == refreshTokenHash);
            query.Query.Include(s => s.User);
            return query;
        }
    }
}