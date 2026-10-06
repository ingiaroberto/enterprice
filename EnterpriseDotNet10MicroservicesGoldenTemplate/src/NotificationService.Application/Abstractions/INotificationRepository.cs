using NotificationService.Domain.Entities;
namespace NotificationService.Application.Abstractions;
public interface INotificationRepository
{
    Task AddAsync(Notification entity,CancellationToken cancellationToken);
}
