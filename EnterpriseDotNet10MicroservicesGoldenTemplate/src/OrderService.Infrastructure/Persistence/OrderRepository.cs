using System.Data;
using Dapper;
using OrderService.Application.Abstractions;
using OrderService.Domain.Entities;
namespace OrderService.Infrastructure.Persistence;
public sealed class OrderRepository : IOrderRepository
{
    private readonly OrderDb _db;
    public OrderRepository(OrderDb db)
    {
        _db = db;
    }
    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await _db.Connection.ExecuteAsync(
        new CommandDefinition(
        "dbo.usp_Order_Create",
        new
        {
            order.Id,
            order.CategoryId,
            order.CreatedAtUtc
        }
        ,
        _db.Transaction,
        commandType: CommandType.StoredProcedure,
        cancellationToken: cancellationToken));
    }
}
