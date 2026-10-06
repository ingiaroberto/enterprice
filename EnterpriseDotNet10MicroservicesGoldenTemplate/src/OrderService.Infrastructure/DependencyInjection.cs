using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Abstractions;
using OrderService.Infrastructure.Http;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Persistence;
namespace OrderService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration) {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));
        services.AddSingleton<RabbitMqConnection>();
        services.AddScoped<OrderDb>();
        services.AddScoped<IOrderRepository,OrderRepository>();
        services.AddScoped<IOutboxRepository,OutboxRepository>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        services.AddHostedService<OutboxPublisher>();
        var baseUrl=configuration["Services:Catalog:BaseUrl"]??throw new InvalidOperationException("Services:Catalog:BaseUrl is required.");
        services.AddHttpClient<ICatalogPort,CatalogClient>(client=>client.BaseAddress=new Uri(baseUrl)).AddStandardResilienceHandler();
        return services;
    }
    private sealed class UnitOfWork(OrderDb db):IUnitOfWork {
        public Task BeginTransactionAsync(CancellationToken c)=>db.BeginAsync(c);
        public Task CommitAsync(CancellationToken c)=>db.CommitAsync(c);
        public Task RollbackAsync(CancellationToken c)=>db.RollbackAsync(c);
    }
}
