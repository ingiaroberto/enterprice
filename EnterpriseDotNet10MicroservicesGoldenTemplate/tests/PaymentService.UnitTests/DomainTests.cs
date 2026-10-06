using Xunit;
using PaymentService.Domain.Entities;
namespace PaymentService.UnitTests;
public sealed class PaymentTests {
    [Fact]public void Create_RejectsNonPositiveAmount() {
        Assert.Throws<ArgumentOutOfRangeException>(()=>Payment.Create(Guid.NewGuid(),Guid.NewGuid(),0,DateTime.UtcNow));
    }
}
