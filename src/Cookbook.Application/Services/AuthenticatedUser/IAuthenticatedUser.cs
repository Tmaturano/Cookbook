using Cookbook.Domain.Entities;

namespace Cookbook.Application.Services.AuthenticatedUser;

public interface IAuthenticatedUser
{
    Task<User> GetAsync();
}
