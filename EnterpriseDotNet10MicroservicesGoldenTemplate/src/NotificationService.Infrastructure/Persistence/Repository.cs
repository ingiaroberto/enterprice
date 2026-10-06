using System.Data;
using Dapper;
using NotificationService.Application.Abstractions;
using NotificationService.Domain.Entities;
namespace NotificationService.Infrastructure.Persistence;
public sealed class Repository:INotificationRepository
{
    private readonly DbSession _db;
    public Repository(DbSession db)=>_db=db;
    public async Task AddAsync(Notification entity,CancellationToken cancellationToken) {
        await _db.Connection.ExecuteAsync(new CommandDefinition("dbo.usp_Notification_Notification_Create",new {
            entity.Id,entity.Recipient,entity.Message,entity.CreatedAtUtc
        }
        ,_db.Transaction,commandType:CommandType.StoredProcedure,cancellationToken:cancellationToken));
    }
}
