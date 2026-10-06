using System.Data;
using System.Text;
using Dapper;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Messaging;

public sealed class OutboxPublisher : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RabbitMqConnection _rabbitMq;
    private readonly RabbitMqOptions _options;
    private readonly ILogger<OutboxPublisher> _logger;

    public OutboxPublisher(
        IServiceScopeFactory scopeFactory,
        RabbitMqConnection rabbitMq,
        IOptions<RabbitMqOptions> options,
        ILogger<OutboxPublisher> logger)
    {
        _scopeFactory = scopeFactory;
        _rabbitMq = rabbitMq;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PublishAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Order outbox publication cycle failed.");
            }

            await Task.Delay(
                TimeSpan.FromSeconds(_options.PollIntervalSeconds),
                stoppingToken);
        }
    }

    private async Task PublishAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<OrderDb>();

        await db.Connection.OpenAsync(cancellationToken);

        var command = new CommandDefinition(
            "dbo.usp_Outbox_GetPending",
            new { BatchSize = _options.PublisherBatchSize },
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var messages = (await db.Connection.QueryAsync<OutboxRow>(command)).AsList();

        if (messages.Count == 0)
        {
            return;
        }

        var connection = await _rabbitMq.GetAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(
            new CreateChannelOptions
            {
                PublisherConfirmationsEnabled = true,
                PublisherConfirmationTrackingEnabled = true
            },
            cancellationToken);

        await channel.ExchangeDeclareAsync(
            _options.Exchange,
            ExchangeType.Topic,
            durable: true,
            autoDelete: false,
            cancellationToken: cancellationToken);

        foreach (var message in messages)
        {
            var properties = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json",
                MessageId = message.Id.ToString(),
                Type = message.Type
            };

            await channel.BasicPublishAsync(
                _options.Exchange,
                message.Type,
                mandatory: false,
                properties,
                Encoding.UTF8.GetBytes(message.Payload),
                cancellationToken);

            var markPublished = new CommandDefinition(
                "dbo.usp_Outbox_MarkPublished",
                new
                {
                    message.Id,
                    PublishedAtUtc = DateTime.UtcNow
                },
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            await db.Connection.ExecuteAsync(markPublished);
        }
    }

    private sealed record OutboxRow(
        Guid Id,
        string Type,
        string Payload,
        DateTime OccurredAtUtc);
}
