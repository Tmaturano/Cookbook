using Cookbook.Application.Services.JWT;
using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;

namespace Cookbook.Application.Services.AuthenticatedUser;

public class AuthenticatedUser : IAuthenticatedUser
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IUserRepository userRepository)
    {
        _contextAccessor = httpContextAccessor;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<User> GetAsync()
    {
        var authorization = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        var token = authorization["Bearer".Length..].Trim();

        var userEmail = _tokenService.RecoverEmail(token);
            
        return await _userRepository.GetByEmailAsync(userEmail);        
    }
}
