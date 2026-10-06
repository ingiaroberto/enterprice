using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace OrderService.Infrastructure.Persistence;
public sealed class OrderDb:IAsyncDisposable
{
    private readonly string _connectionString;
    private DbConnection? _connection;
    private DbTransaction? _transaction;
    public OrderDb(IConfiguration c)
    {
        _connectionString = c.GetConnectionString("OrderDatabase")
            ?? throw new InvalidOperationException("ConnectionStrings:OrderDatabase is required.");
    }
    public DbConnection Connection=>_connection??=new SqlConnection(_connectionString);
    public DbTransaction? Transaction=>_transaction;
    public async Task BeginAsync(CancellationToken ct) {
        if (_transaction is not null)throw new InvalidOperationException("Transaction already active.");
        await Connection.OpenAsync(ct);
        _transaction=await Connection.BeginTransactionAsync(ct);
    }
    public async Task CommitAsync(CancellationToken ct) {
        if (_transaction is null)throw new InvalidOperationException("No active transaction.");
        await _transaction.CommitAsync(ct);
        await DisposeTransactionAsync();
    }
    public async Task RollbackAsync(CancellationToken ct) {
        if (_transaction is null)return;
        await _transaction.RollbackAsync(ct);
        await DisposeTransactionAsync();
    }
    private async Task DisposeTransactionAsync() {
        if (_transaction is not null)await _transaction.DisposeAsync();
        _transaction=null;
    }
    public async ValueTask DisposeAsync() {
        await DisposeTransactionAsync();
        if (_connection is not null)await _connection.DisposeAsync();
    }
}
