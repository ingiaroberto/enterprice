using FluentValidation;
using MediatR;
using InventoryService.Application.Abstractions;
using InventoryService.Domain.Entities;
namespace InventoryService.Application.Features;
public sealed record CreateInventoryCommand(Guid ProductId, int Quantity) : IRequest<CreateInventoryResult>;
public sealed record CreateInventoryResult(Guid Id);
public sealed class CreateInventoryValidator:AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryValidator() {
        RuleFor(x=>x.ProductId).NotEmpty();
        RuleFor(x=>x.Quantity).GreaterThanOrEqualTo(0);
    }
}
public sealed class CreateInventoryHandler:IRequestHandler<CreateInventoryCommand,CreateInventoryResult>
{
    private readonly IInventoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TimeProvider _timeProvider;
    public CreateInventoryHandler(IInventoryRepository repository,IUnitOfWork unitOfWork,TimeProvider timeProvider) {
        _repository=repository;
        _unitOfWork=unitOfWork;
        _timeProvider=timeProvider;
    }
    public async Task<CreateInventoryResult> Handle(CreateInventoryCommand request,CancellationToken cancellationToken) {
        var now=_timeProvider.GetUtcNow().UtcDateTime;
        var entity=InventoryItem.Create(Guid.NewGuid(), request.ProductId, request.Quantity, now);
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try {
            await _repository.AddAsync(entity,cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new(entity.Id);
        }
                                                catch {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
