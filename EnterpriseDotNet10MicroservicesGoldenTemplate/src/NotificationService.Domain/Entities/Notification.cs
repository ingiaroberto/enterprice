namespace NotificationService.Domain.Entities;
public sealed class Notification
{
    private Notification(Guid id,string recipient,string message,DateTime createdAtUtc) {
        if (id==Guid.Empty)throw new ArgumentException("Notification id is required.",nameof(id));
        if (string.IsNullOrWhiteSpace(recipient))throw new ArgumentException("Recipient is required.",nameof(recipient));
        if (string.IsNullOrWhiteSpace(message))throw new ArgumentException("Message is required.",nameof(message));
        Id=id;
        Recipient=recipient.Trim();
        Message=message.Trim();
        CreatedAtUtc=createdAtUtc;
    }
    public Guid Id {
        get;
    }
    public string Recipient {
        get;
    }
    public string Message {
        get;
    }
    public DateTime CreatedAtUtc {
        get;
    }
    public static Notification Create(Guid id,string recipient,string message,DateTime createdAtUtc)=>new(id,recipient,message,createdAtUtc);
}
