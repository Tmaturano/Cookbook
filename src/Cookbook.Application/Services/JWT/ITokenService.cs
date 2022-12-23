using Cookbook.Domain.Entities;
using System.Security.Claims;

namespace Cookbook.Application.Services.JWT;

public interface ITokenService
{
    string GenerateToken(User user);
    ClaimsPrincipal ValidateToken(string token);
    string RecoverEmail(string token);
}
