using Enterprise.Observability;
using Enterprise.Security;
using FluentValidation;
using MediatR;
using NotificationService.Application.Features;
using NotificationService.Infrastructure;
var builder=WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<NotificationService.Api.Middleware.GlobalExceptionHandler>();
builder.Services.AddMediatR(configuration =>
    configuration
        .RegisterServicesFromAssemblyContaining<CreateNotificationCommand>()
        .AddOpenBehavior(typeof(NotificationService.Application.Behaviors.ValidationBehavior<,>)));
builder.Services.AddValidatorsFromAssemblyContaining<CreateNotificationValidator>();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddInfrastructure();
builder.Services.AddEnterpriseObservability();
builder.Services.AddEnterpriseAuthentication(builder.Configuration);
builder.Services.AddHealthChecks();
var app=builder.Build();
var authenticationConfigured = !string.IsNullOrWhiteSpace(builder.Configuration["Authentication:Authority"]) &&
!string.IsNullOrWhiteSpace(builder.Configuration["Authentication:Audience"]);
if (!app.Environment.IsDevelopment() && !authenticationConfigured)
{
    throw new InvalidOperationException("Authentication:Authority and Authentication:Audience are required outside Development.");
}
app.UseExceptionHandler();
app.UseEnterpriseObservability();
app.UseEnterpriseSecurity();
app.UseRateLimiter();
var endpoints = app.MapControllers().RequireRateLimiting("api");
if (authenticationConfigured)
{
    endpoints.RequireAuthorization();
}
app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready");
app.Run();
public partial class Program;
