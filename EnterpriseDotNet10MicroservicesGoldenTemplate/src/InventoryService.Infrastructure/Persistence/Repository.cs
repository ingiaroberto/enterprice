using System.Data;
using Dapper;
using InventoryService.Application.Abstractions;
using InventoryService.Domain.Entities;
namespace InventoryService.Infrastructure.Persistence;
public sealed class Repository:IInventoryRepository
{
    private readonly DbSession _db;
    public Repository(DbSession db)=>_db=db;
    public async Task AddAsync(InventoryItem entity,CancellationToken cancellationToken) {
        await _db.Connection.ExecuteAsync(new CommandDefinition("dbo.usp_Inventory_InventoryItem_Create",new {
            entity.Id,entity.ProductId,entity.Quantity,entity.UpdatedAtUtc
        }
        ,_db.Transaction,commandType:CommandType.StoredProcedure,cancellationToken:cancellationToken));
    }
}
