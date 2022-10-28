namespace Cookbook.Domain.Entities;

public class User : EntityBase
{
    public User(string name, string email, string passwordHash, string phone)
    {           
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone;
    }    
    
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Phone { get; private set; }
}
