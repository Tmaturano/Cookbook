using Microsoft.EntityFrameworkCore;

namespace Cookbook.Infrastructure.Data;

public class CookbookContext : DbContext
{
    public CookbookContext(DbContextOptions<CookbookContext> options) : base(options)
    {

    }
}
