using System.Data;
using Dapper;
using PaymentService.Application.Abstractions;
using PaymentService.Domain.Entities;
namespace PaymentService.Infrastructure.Persistence;
public sealed class Repository:IPaymentRepository
{
    private readonly DbSession _db;
    public Repository(DbSession db)=>_db=db;
    public async Task AddAsync(Payment entity,CancellationToken cancellationToken) {
        await _db.Connection.ExecuteAsync(new CommandDefinition("dbo.usp_Payment_Payment_Create",new {
            entity.Id,entity.OrderId,entity.Amount,entity.CreatedAtUtc
        }
        ,_db.Transaction,commandType:CommandType.StoredProcedure,cancellationToken:cancellationToken));
    }
}
