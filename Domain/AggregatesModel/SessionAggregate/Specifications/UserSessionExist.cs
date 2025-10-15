using Ardalis.Specification;
using Domain.Common.Extensions;

namespace Domain.AggregatesModel.SessionAggregate.Specifications
{
    public class UserSessionExist : Specification<UserSession>
    {
        public UserSessionExist(long userId, string deviceInfo, string ipAddress)
        {
            Query.Where(s => s.UserId == userId && s.DeviceInfo == deviceInfo && s.IPAddress == ipAddress && !s.IsExpired());
        }
    }
}