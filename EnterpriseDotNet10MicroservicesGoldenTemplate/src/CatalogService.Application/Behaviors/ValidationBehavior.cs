using FluentValidation;
using MediatR;
namespace CatalogService.Application.Behaviors;
public sealed class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest:notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)=>_validators=validators;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validators=_validators.ToArray();
        if (validators.Length==0)return await next();
        var contexts=validators.Select(v=>v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken));
        var failures=(await Task.WhenAll(contexts)).SelectMany(r=>r.Errors).Where(e=>e is not null).ToArray();
        if (failures.Length>0)throw new ValidationException(failures);
        return await next();
    }
}
