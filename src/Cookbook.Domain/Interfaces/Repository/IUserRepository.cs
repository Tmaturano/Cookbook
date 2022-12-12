using Cookbook.Domain.Entities;

namespace Cookbook.Domain.Interfaces.Repository;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<bool> ExistsAsync(string email);
    Task<User> GetByEmailAndPasswordAsync(string email, string password);
}
