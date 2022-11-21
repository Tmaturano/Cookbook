using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;

namespace Cookbook.Infrastructure.Data.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
	public UserRepository(CookbookContext context) : base(context)
	{
	}

}
