using System.Text.Json;
using FluentValidation;
using MediatR;
using OrderService.Application.Abstractions;
using OrderService.Domain.Entities;
namespace OrderService.Application.Features;
public sealed record CreateOrderCommand(Guid CategoryId) : IRequest<CreateOrderResult>;
public sealed record CreateOrderResult(Guid Id, Guid CategoryId, DateTime CreatedAtUtc);
public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}
public sealed class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orders;
    private readonly ICatalogPort _catalog;
    private readonly IOutboxRepository _outbox;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TimeProvider _time;
    public CreateOrderHandler(
    IOrderRepository orders,
    ICatalogPort catalog,
    IOutboxRepository outbox,
    IUnitOfWork unitOfWork,
    TimeProvider time)
    {
        _orders = orders;
        _catalog = catalog;
        _outbox = outbox;
        _unitOfWork = unitOfWork;
        _time = time;
    }
    public async Task<CreateOrderResult> Handle(
    CreateOrderCommand request,
    CancellationToken cancellationToken)
    {
        var category = await _catalog.GetCategoryAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            throw new KeyNotFoundException($"Category '{request.CategoryId}' was not found.");
        }
        if (!category.Active)
        {
            throw new InvalidOperationException($"Category '{request.CategoryId}' is inactive.");
        }
        var createdAtUtc = _time.GetUtcNow().UtcDateTime;
        var order = Order.Create(Guid.NewGuid(), category.Id, createdAtUtc);
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _orders.AddAsync(order, cancellationToken);
            var eventPayload = new
            {
                EventId = Guid.NewGuid(),
                OrderId = order.Id,
                CategoryId = order.CategoryId,
                OccurredAtUtc = order.CreatedAtUtc
            }
            ;
            await _outbox.AddAsync(
            new OutboxMessage(
            eventPayload.EventId,
            "order.created.v1",
            JsonSerializer.Serialize(eventPayload),
            order.CreatedAtUtc),
            cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new(order.Id, order.CategoryId, order.CreatedAtUtc);
        }
                                                catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
