using PaymentService.Domain.Entities;
namespace PaymentService.Application.Abstractions;
public interface IPaymentRepository
{
    Task AddAsync(Payment entity,CancellationToken cancellationToken);
}
