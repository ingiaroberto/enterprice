namespace PaymentService.Domain.Entities;
public sealed class Payment
{
    private Payment(Guid id,Guid orderId,decimal amount,DateTime createdAtUtc) {
        if (id==Guid.Empty)throw new ArgumentException("Payment id is required.",nameof(id));
        if (orderId==Guid.Empty)throw new ArgumentException("Order id is required.",nameof(orderId));
        if (amount<=0)throw new ArgumentOutOfRangeException(nameof(amount));
        Id=id;
        OrderId=orderId;
        Amount=amount;
        CreatedAtUtc=createdAtUtc;
    }
    public Guid Id {
        get;
    }
    public Guid OrderId {
        get;
    }
    public decimal Amount {
        get;
    }
    public DateTime CreatedAtUtc {
        get;
    }
    public static Payment Create(Guid id,Guid orderId,decimal amount,DateTime createdAtUtc)=>new(id,orderId,amount,createdAtUtc);
}
