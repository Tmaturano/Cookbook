using Cookbook.Application.Services.JWT;
using Cookbook.Communication.Response;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Cookbook.API.Filters;

public class AuthenticatedUserAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AuthenticatedUserAttribute(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = GetToken(context);
            var email = _tokenService.RecoverEmail(token);

            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null)
                throw new Exception();
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpired(context);
        }
        catch
        {
            UserHasNoAccess(context);
        }


    }

    private string GetToken(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
            throw new Exception();

        return authorization["Bearer".Length..].Trim();
    }

    private void TokenExpired(AuthorizationFilterContext context) 
        => context.Result = new UnauthorizedObjectResult(new ErrorResponse(ErrorMessages.TokenExpired));

    private void UserHasNoAccess(AuthorizationFilterContext context)
        => context.Result = new UnauthorizedObjectResult(new ErrorResponse(ErrorMessages.UserHasNoAccess));
}
