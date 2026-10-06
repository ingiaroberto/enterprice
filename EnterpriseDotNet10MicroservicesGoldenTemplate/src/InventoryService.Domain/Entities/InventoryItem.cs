namespace InventoryService.Domain.Entities;
public sealed class InventoryItem
{
    private InventoryItem(Guid id,Guid productId,int quantity,DateTime updatedAtUtc) {
        if (id==Guid.Empty)throw new ArgumentException("Inventory id is required.",nameof(id));
        if (productId==Guid.Empty)throw new ArgumentException("Product id is required.",nameof(productId));
        if (quantity<0)throw new ArgumentOutOfRangeException(nameof(quantity));
        Id=id;
        ProductId=productId;
        Quantity=quantity;
        UpdatedAtUtc=updatedAtUtc;
    }
    public Guid Id {
        get;
    }
    public Guid ProductId {
        get;
    }
    public int Quantity {
        get;
    }
    public DateTime UpdatedAtUtc {
        get;
    }
    public static InventoryItem Create(Guid id,Guid productId,int quantity,DateTime updatedAtUtc)=>new(id,productId,quantity,updatedAtUtc);
}
