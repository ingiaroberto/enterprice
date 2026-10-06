using NotificationService.Application.Abstractions;
using NotificationService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
namespace NotificationService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
        services.AddScoped<DbSession>();
        services.AddScoped<INotificationRepository,Repository>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        return services;
    }
    private sealed class UnitOfWork:IUnitOfWork {
        private readonly DbSession _db;
        public UnitOfWork(DbSession db)=>_db=db;
        public Task BeginTransactionAsync(CancellationToken c)=>_db.BeginAsync(c);
        public Task CommitAsync(CancellationToken c)=>_db.CommitAsync(c);
        public Task RollbackAsync(CancellationToken c)=>_db.RollbackAsync(c);
    }
}
