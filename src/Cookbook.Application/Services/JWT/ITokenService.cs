using Cookbook.Domain.Entities;

namespace Cookbook.Application.Services.JWT;

public interface ITokenService
{
    string GenerateToken(User user);
    void ValidateToken(string token);
}
