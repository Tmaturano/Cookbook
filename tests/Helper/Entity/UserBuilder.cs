using AutoBogus;
using Cookbook.Domain.Entities;
using SecureIdentity.Password;

namespace Helper.Entity;

public static class UserBuilder
{
    public static (User user, string password) Build()
    {
        var password = string.Empty;

        var generatedUser = new AutoFaker<User>()
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(1, 9)}"))
            .RuleFor(u => u.PasswordHash, f =>
            {
                password = f.Internet.Password();

                return PasswordHasher.Hash(password);
            });

        return (generatedUser, password);
    }
}
