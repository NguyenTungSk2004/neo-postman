using Ardalis.Specification;
namespace Domain.AggregatesModel.VerificationAggregate.Specifications;

public class UserVerificationTokenByUserId : Specification<UserVerificationToken>
{
    public UserVerificationTokenByUserId(long userId, TypeOfVerificationToken type)
    {
        Query.Where(t => t.UserId == userId && t.Type == type && t.IsValid());
    }
}
