using FluentValidation;
using MediatR;
namespace NotificationService.Application.Behaviors;
public sealed class ValidationBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse> where TRequest:notnull {
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)=>_validators=validators;
    public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,CancellationToken cancellationToken) {
        var validators=_validators.ToArray();
        if (validators.Length==0)return await next();
        var results=await Task.WhenAll(validators.Select(v=>v.ValidateAsync(new ValidationContext<TRequest>(request),cancellationToken)));
        var failures=results.SelectMany(r=>r.Errors).Where(e=>e is not null).ToArray();
        if (failures.Length>0)throw new ValidationException(failures);
        return await next();
    }
}
