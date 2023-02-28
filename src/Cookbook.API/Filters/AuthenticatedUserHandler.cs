using Cookbook.Application.Services.AuthenticatedUser;
using Microsoft.AspNetCore.Authorization;

namespace Cookbook.API.Filters;

public class AuthenticatedUserHandler : AuthorizationHandler<AuthenticatedUserRequirement>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public AuthenticatedUserHandler(IAuthenticatedUser authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthenticatedUserRequirement requirement)
    {
        try
        {
            var userExists = await _authenticatedUser.GetAsync() is not null;
            if (userExists)
                context.Succeed(requirement);
            else
                context.Fail();
        }
        catch (Exception)
        {
            context.Fail();
        }
    }

}
