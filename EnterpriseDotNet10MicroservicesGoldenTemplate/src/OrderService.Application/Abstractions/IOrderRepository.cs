using OrderService.Domain.Entities;
namespace OrderService.Application.Abstractions;
public interface IOrderRepository {
    Task AddAsync(Order order,CancellationToken cancellationToken);
}
