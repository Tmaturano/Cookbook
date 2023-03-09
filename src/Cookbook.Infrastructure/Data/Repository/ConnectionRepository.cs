using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Infrastructure.Data.Repository;

public class ConnectionRepository : RepositoryBase<Connection>, IConnectionRepository
{
    public ConnectionRepository(CookbookContext context) : base(context)
    {
    }

    public async Task<bool> ExistsBetween(Guid userId, Guid connectedWithUserId) 
        => await DbSet.AnyAsync(x => x.UserId == userId && x.ConnectedWithUserId == connectedWithUserId);

    public async Task RegisterAsync(Connection connection) 
        => await DbSet.AddAsync(connection);
}
