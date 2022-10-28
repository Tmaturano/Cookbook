using Cookbook.Domain.Entities;
using Cookbook.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Infrastructure.Data;

public class CookbookContext : DbContext
{
    public CookbookContext(DbContextOptions<CookbookContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}
