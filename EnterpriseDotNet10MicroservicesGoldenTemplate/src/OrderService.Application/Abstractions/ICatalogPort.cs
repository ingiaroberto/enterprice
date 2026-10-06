namespace OrderService.Application.Abstractions;
public interface ICatalogPort
{
    Task<CatalogCategory?> GetCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
}
public sealed record CatalogCategory(Guid Id, string Name, bool Active);
