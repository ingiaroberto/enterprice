using Xunit;
using InventoryService.Domain.Entities;
namespace InventoryService.UnitTests;
public sealed class InventoryTests {
    [Fact]public void Create_RejectsNegativeQuantity() {
        Assert.Throws<ArgumentOutOfRangeException>(()=>InventoryItem.Create(Guid.NewGuid(),Guid.NewGuid(),-1,DateTime.UtcNow));
    }
}
