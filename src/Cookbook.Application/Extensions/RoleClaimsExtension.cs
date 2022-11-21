using Cookbook.Domain.Entities;
using System.Security.Claims;

namespace Cookbook.Application.Extensions;

public static class RoleClaimsExtension
{
    /// <summary>
    /// return Claims of a user
    /// Can add more claims such as the user Roles (if exists)
    /// e.g. result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));
    /// </summary>
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
        };
    }
}
