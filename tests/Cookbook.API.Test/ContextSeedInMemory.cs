using Cookbook.Domain.Entities;
using Cookbook.Infrastructure.Data;
using Helper.Entity;

namespace Cookbook.API.Test;

public static class ContextSeedInMemory
{
    public static (User user, string password) Seed(CookbookContext context)
    {
        (var user, var password) = UserBuilder.Build();

        context.Users.Add(user);

        context.SaveChanges();

        return (user, password);
    }
}
