using System.Data;
using CatalogService.Application.Abstractions;
using CatalogService.Domain.Entities;
using Dapper;
namespace CatalogService.Infrastructure.Persistence;
public sealed class CatalogRepository : ICatalogRepository
{
    private readonly CatalogDb _db;
    public CatalogRepository(CatalogDb db)
    {
        _db = db;
    }
    public async Task<Category?> GetCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        var row = await _db.Connection.QuerySingleOrDefaultAsync<CategoryRow>(
        new CommandDefinition(
        "dbo.usp_Catalog_Category_GetById",
        new {
            CategoryId = id
        }
        ,
        _db.Transaction,
        commandType: CommandType.StoredProcedure,
        cancellationToken: cancellationToken));
        return row is null
        ? null
        : Category.Rehydrate(row.Id, row.Name, row.Active, row.CreatedAtUtc);
    }
    public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        await _db.Connection.ExecuteAsync(
        new CommandDefinition(
        "dbo.usp_Catalog_Category_Create",
        new
        {
            category.Id,
            category.Name,
            category.Active,
            category.CreatedAtUtc
        }
        ,
        _db.Transaction,
        commandType: CommandType.StoredProcedure,
        cancellationToken: cancellationToken));
    }
    private sealed record CategoryRow(
    Guid Id,
    string Name,
    bool Active,
    DateTime CreatedAtUtc);
}
