using System.Net;
using System.Net.Http.Json;
using OrderService.Application.Abstractions;
namespace OrderService.Infrastructure.Http;
public sealed class CatalogClient(HttpClient client) : ICatalogPort
{
    public async Task<CatalogCategory?> GetCategoryAsync(
    Guid categoryId,
    CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync(
        $"api/catalog/categories/{categoryId}",
        cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CatalogCategory>(
        cancellationToken: cancellationToken);
    }
}
