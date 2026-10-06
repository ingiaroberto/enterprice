using Xunit;
using NotificationService.Domain.Entities;
namespace NotificationService.UnitTests;
public sealed class NotificationTests {
    [Fact]public void Create_TrimsRecipient() {
        var n=Notification.Create(Guid.NewGuid()," user@example.com ","hello",DateTime.UtcNow);
        Assert.Equal("user@example.com",n.Recipient);
    }
}
