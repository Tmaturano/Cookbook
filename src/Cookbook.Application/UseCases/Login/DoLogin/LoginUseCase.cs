using Cookbook.Application.Services.JWT;
using Cookbook.Application.UseCases.User.Create;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Exceptions.ExceptionsBase;
using Cookbook.Exceptions;
using SecureIdentity.Password;
using Cookbook.Domain.Entities;
using FluentValidation.Results;

namespace Cookbook.Application.UseCases.Login.DoLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
    {        
        var user = await _userRepository.GetByEmailAsync(request.Email);

        await ValidateAsync(request, user);

        return new LoginResponse(Token: _tokenService.GenerateToken(user), Name: user.Name);

    }

    private async Task ValidateAsync(LoginRequest request, Domain.Entities.User user)
    {
        var validator = new LoginValidator();
        var result = await validator.ValidateAsync(request);

        if (user is null)
        {
            result.Errors.Add(new ValidationFailure("user", "User not found"));
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new InvalidLoginException(errorMessages);
        }

        if (!PasswordHasher.Verify(user.PasswordHash, request.Password))
            result.Errors.Add(new ValidationFailure("password", ErrorMessages.InvalidUserPassword));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
