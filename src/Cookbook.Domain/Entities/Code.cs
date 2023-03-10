namespace Cookbook.Domain.Entities;

public class Code : EntityBase
{
    public Code(string value, Guid userId)
    {
        Value = value;
        UserId = userId;
    }

    public string Value { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public void SetValue(string value) => Value = value;
}
