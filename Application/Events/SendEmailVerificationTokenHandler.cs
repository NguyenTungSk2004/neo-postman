using Application.Common.DomainEvent;
using MediatR;

namespace Domain.Events;

public class SendEmailVerificationTokenHandler : INotificationHandler<DomainEventNotification<SendEmailVerificationToken>>
{
    public async Task Handle(DomainEventNotification<SendEmailVerificationToken> notification, CancellationToken cancellationToken)
    {
        var token = notification.DomainEvent.Token;

        await Task.CompletedTask;
    }
}