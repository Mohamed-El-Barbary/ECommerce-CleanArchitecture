namespace ECommerce.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset UpdatedAt { get; protected set; }
    public bool IsDeleted { get; private set; }

    // ToDo => CreateById - UpdatedById 

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.Now;
    }
}
