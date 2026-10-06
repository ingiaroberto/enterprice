using FluentValidation;
using MediatR;
using NotificationService.Application.Abstractions;
using NotificationService.Domain.Entities;
namespace NotificationService.Application.Features;
public sealed record CreateNotificationCommand(string Recipient, string Message) : IRequest<CreateNotificationResult>;
public sealed record CreateNotificationResult(Guid Id);
public sealed class CreateNotificationValidator:AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationValidator() {
        RuleFor(x=>x.Recipient).NotEmpty().MaximumLength(320);
        RuleFor(x=>x.Message).NotEmpty().MaximumLength(4000);
    }
}
public sealed class CreateNotificationHandler:IRequestHandler<CreateNotificationCommand,CreateNotificationResult>
{
    private readonly INotificationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TimeProvider _timeProvider;
    public CreateNotificationHandler(INotificationRepository repository,IUnitOfWork unitOfWork,TimeProvider timeProvider) {
        _repository=repository;
        _unitOfWork=unitOfWork;
        _timeProvider=timeProvider;
    }
    public async Task<CreateNotificationResult> Handle(CreateNotificationCommand request,CancellationToken cancellationToken) {
        var now=_timeProvider.GetUtcNow().UtcDateTime;
        var entity=Notification.Create(Guid.NewGuid(), request.Recipient, request.Message, now);
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
