using Domain.AggregatesModel.VerificationAggregate;
using Domain.SeedWork;

namespace Domain.Events;

public class SendEmailVerificationToken : IDomainEvent
{
    public UserVerificationToken Token { get; }

    public SendEmailVerificationToken(UserVerificationToken token)
    {
        Token = token;
    }
}