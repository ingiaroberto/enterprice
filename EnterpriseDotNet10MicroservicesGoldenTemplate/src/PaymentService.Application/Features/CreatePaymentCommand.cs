using FluentValidation;
using MediatR;
using PaymentService.Application.Abstractions;
using PaymentService.Domain.Entities;
namespace PaymentService.Application.Features;
public sealed record CreatePaymentCommand(Guid OrderId, decimal Amount) : IRequest<CreatePaymentResult>;
public sealed record CreatePaymentResult(Guid Id);
public sealed class CreatePaymentValidator:AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentValidator() {
        RuleFor(x=>x.OrderId).NotEmpty();
        RuleFor(x=>x.Amount).GreaterThan(0);
    }
}
public sealed class CreatePaymentHandler:IRequestHandler<CreatePaymentCommand,CreatePaymentResult>
{
    private readonly IPaymentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TimeProvider _timeProvider;
    public CreatePaymentHandler(IPaymentRepository repository,IUnitOfWork unitOfWork,TimeProvider timeProvider) {
        _repository=repository;
        _unitOfWork=unitOfWork;
        _timeProvider=timeProvider;
    }
    public async Task<CreatePaymentResult> Handle(CreatePaymentCommand request,CancellationToken cancellationToken) {
        var now=_timeProvider.GetUtcNow().UtcDateTime;
        var entity=Payment.Create(Guid.NewGuid(), request.OrderId, request.Amount, now);
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
