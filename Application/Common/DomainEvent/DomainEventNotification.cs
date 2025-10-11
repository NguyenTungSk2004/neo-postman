using Domain.SeedWork;
using MediatR;

namespace Application.Common.DomainEvent;
public record DomainEventNotification<TDomainEvent>(TDomainEvent DomainEvent): INotification where TDomainEvent : IDomainEvent;