using Ardalis.Specification;
namespace Domain.AggregatesModel.VerificationAggregate.Specifications;

public class UserVerificationTokenSpecification : Specification<UserVerificationToken>
{
    public UserVerificationTokenSpecification(TypeOfVerificationToken type)
    {
        Query.Where(x => x.Type == type && x.ExpiresAt > DateTimeOffset.UtcNow && !x.UsedAt.HasValue);
    }

    public static UserVerificationTokenSpecification ByUserId(long userId, TypeOfVerificationToken type)
    {
        var query = new UserVerificationTokenSpecification(type);
        query.Query.Where(x => x.UserId == userId);
        return query;
    }
    public static UserVerificationTokenSpecification ByToken(string token,  TypeOfVerificationToken type)
    {
        var query = new UserVerificationTokenSpecification(type);
        query.Query.Where(x => x.Token == token);
        query.Query.Include(x => x.User);
        return query;
    }
}
