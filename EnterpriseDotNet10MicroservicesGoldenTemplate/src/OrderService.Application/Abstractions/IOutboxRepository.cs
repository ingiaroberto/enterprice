namespace OrderService.Application.Abstractions;
public interface IOutboxRepository {
    Task AddAsync(OutboxMessage message,CancellationToken cancellationToken);
}
public sealed record OutboxMessage(Guid Id,string Type,string Payload,DateTime OccurredAtUtc);
