using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Enterprise.Observability;
public static class ObservabilityExtensions
{
    public const string CorrelationHeader = "X-Correlation-Id";
    public static IServiceCollection AddEnterpriseObservability(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        return services;
    }
    public static IApplicationBuilder UseEnterpriseObservability(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var correlationId = context.Request.Headers[CorrelationHeader].FirstOrDefault();
            if (!Guid.TryParse(correlationId, out _))
            {
                correlationId = Guid.NewGuid().ToString("N");
            }
            context.Response.Headers[CorrelationHeader] = correlationId;
            using (context.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger("Correlation")
            .BeginScope(new Dictionary<string, object> {
                ["CorrelationId"] = correlationId
            }
            ))
            {
                await next();
            }
        }
        );
    }
}
