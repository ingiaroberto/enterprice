using InventoryService.Domain.Entities;
namespace InventoryService.Application.Abstractions;
public interface IInventoryRepository
{
    Task AddAsync(InventoryItem entity,CancellationToken cancellationToken);
}
