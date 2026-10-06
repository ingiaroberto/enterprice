using OrderService.Domain.Entities;
using Xunit;
namespace OrderService.UnitTests;
public sealed class DomainTests
{
    [Fact]
    public void Create_WithCategoryId_CreatesOrder()
    {
        var categoryId = Guid.NewGuid();
        var order = Order.Create(Guid.NewGuid(), categoryId, DateTime.UtcNow);
        Assert.Equal(categoryId, order.CategoryId);
    }
    [Fact]
    public void Create_WithEmptyCategoryId_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
        Order.Create(Guid.NewGuid(), Guid.Empty, DateTime.UtcNow));
    }
}
