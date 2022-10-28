namespace Cookbook.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; }
    public DateTime CreatedOn { get; private set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
    }
}
