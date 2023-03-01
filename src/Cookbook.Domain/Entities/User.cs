namespace Cookbook.Domain.Entities;

public class User : EntityBase
{
    protected User()
    {
    }

    public User(string name, string email, string phone)
    {           
        Name = name;
        Email = email;
        Phone = phone;
    }    
    
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Phone { get; private set; }
    public ICollection<Recipe> Recipes { get; private set; } = new List<Recipe>();
    public Code Code { get; private set; }

    public void SetPassword(string password) => PasswordHash = password;
    public void SetCode(Code code) => Code = code;
}
