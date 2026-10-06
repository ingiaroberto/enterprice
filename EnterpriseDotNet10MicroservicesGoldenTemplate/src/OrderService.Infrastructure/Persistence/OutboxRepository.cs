using System.Data;
using Dapper;
using OrderService.Application.Abstractions;
namespace OrderService.Infrastructure.Persistence;
public sealed class OutboxRepository:IOutboxRepository {
    private readonly OrderDb _db;
    public OutboxRepository(OrderDb db)=>_db=db;
    public async Task AddAsync(OutboxMessage message,CancellationToken cancellationToken) {
        await _db.Connection.ExecuteAsync(new CommandDefinition("dbo.usp_Outbox_Add",new {
            message.Id,message.Type,message.Payload,message.OccurredAtUtc
        }
        ,_db.Transaction,commandType:CommandType.StoredProcedure,cancellationToken:cancellationToken));
    }
}
