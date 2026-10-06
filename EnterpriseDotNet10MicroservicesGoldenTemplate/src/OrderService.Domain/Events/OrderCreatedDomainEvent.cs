namespace OrderService.Domain.Events;
public sealed record OrderCreatedDomainEvent(Guid EventId,Guid OrderId,DateTime OccurredAtUtc);
