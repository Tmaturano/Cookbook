namespace Cookbook.Domain.Entities;

public class Connection : EntityBase
{
    public Guid UserId { get; set; }
    public Guid ConnectedWithUserId { get; set; }

    public User User { get; set; }
    public User ConnectedWithUser { get; set; }
}
