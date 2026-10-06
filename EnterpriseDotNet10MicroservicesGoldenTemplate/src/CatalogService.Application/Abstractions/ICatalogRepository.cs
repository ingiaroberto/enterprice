using CatalogService.Domain.Entities;
namespace CatalogService.Application.Abstractions;
public interface ICatalogRepository
{
    Task<Category?> GetCategoryAsync(Guid id, CancellationToken cancellationToken);
    Task AddCategoryAsync(Category category, CancellationToken cancellationToken);
}
