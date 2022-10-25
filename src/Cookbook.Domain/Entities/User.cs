namespace Cookbook.Domain.Entities;

public class User
{
    public User(string name, string email, string passwordHash, string phone)
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Phone { get; private set; }
}
