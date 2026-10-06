using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace InventoryService.Infrastructure.Persistence;
public sealed class DbSession
{
    private readonly string _connectionString;
    private DbConnection? _connection;
    private DbTransaction? _transaction;
    public DbSession(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("InventoryDatabase")
            ?? throw new InvalidOperationException("ConnectionStrings:InventoryDatabase is required.");
    }
    public DbConnection Connection=>_connection??=new SqlConnection(_connectionString);
    public DbTransaction? Transaction=>_transaction;
    public async Task BeginAsync(CancellationToken ct) {
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
