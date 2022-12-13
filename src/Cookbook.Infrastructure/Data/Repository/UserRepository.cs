using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Infrastructure.Data.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
	public UserRepository(CookbookContext context) : base(context)
	{
	}

    public async Task<bool> ExistsAsync(string email) => await DbSet.AnyAsync(x => x.Email == email);

    public async Task<User> GetByEmailAsync(string email) 
		=> await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email));
}
