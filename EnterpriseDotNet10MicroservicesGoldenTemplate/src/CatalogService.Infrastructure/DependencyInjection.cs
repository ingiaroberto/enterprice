using CatalogService.Application.Abstractions;
using CatalogService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
namespace CatalogService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<CatalogDb>();
        services.AddScoped<ICatalogRepository,CatalogRepository>();
        services.AddScoped<IUnitOfWork>(sp=>new CatalogUnitOfWork(sp.GetRequiredService<CatalogDb>()));
        return services;
    }
    private sealed class CatalogUnitOfWork : IUnitOfWork
    {
        private readonly CatalogDb _db;
        public CatalogUnitOfWork(CatalogDb db)=>_db=db;
        public Task BeginTransactionAsync(CancellationToken c)=>_db.BeginAsync(c);
        public Task CommitAsync(CancellationToken c)=>_db.CommitAsync(c);
        public Task RollbackAsync(CancellationToken c)=>_db.RollbackAsync(c);
    }
}
