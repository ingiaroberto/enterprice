namespace OrderService.Domain.Entities;
public sealed class Order
{
    private Order(Guid id, Guid categoryId, DateTime createdAtUtc)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Order id is required.", nameof(id));
        }
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentException("Category id is required.", nameof(categoryId));
        }
        Id = id;
        CategoryId = categoryId;
        CreatedAtUtc = createdAtUtc;
    }
    public Guid Id {
        get;
    }
    public Guid CategoryId {
        get;
    }
    public DateTime CreatedAtUtc {
        get;
    }
    public static Order Create(Guid id, Guid categoryId, DateTime createdAtUtc) =>
    new(id, categoryId, createdAtUtc);
}
