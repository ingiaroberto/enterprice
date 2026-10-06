using CatalogService.Domain.Entities;
using Xunit;
namespace CatalogService.UnitTests;
public sealed class DomainTests
{
    [Fact]
    public void Create_WithValidName_CreatesActiveCategory()
    {
        var category = Category.Create(Guid.NewGuid(), "Beverages", DateTime.UtcNow);
        Assert.True(category.Active);
        Assert.Equal("Beverages", category.Name);
    }
}
