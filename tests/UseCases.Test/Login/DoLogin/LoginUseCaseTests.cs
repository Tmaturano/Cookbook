using Cookbook.Application.Services.JWT;
using Cookbook.Application.UseCases.Login.DoLogin;
using Cookbook.Communication.Request;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Exceptions;
using Cookbook.Exceptions.ExceptionsBase;
using FluentAssertions;
using Helper.Entity;
using Moq;
using System;
using Xunit;

namespace UseCases.Test.Login.DoLogin;

public class LoginUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<ITokenService> _tokenService;
    private readonly LoginUseCase _loginUseCase;

    public LoginUseCaseTests()
    {
        _userRepository = new Mock<IUserRepository>();
        _tokenService = new Mock<ITokenService>();
        _loginUseCase = new LoginUseCase(_userRepository.Object, _tokenService.Object);
    }

    [Fact]
    public async Task Validate_Success()
    {
        //Arrange
        (var user, var password) = UserBuilder.Build();
        _userRepository.Setup(x => x.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _tokenService.Setup(x => x.GenerateToken(user)).Returns("ABCD");

        //Act
        var response = await _loginUseCase.ExecuteAsync(new LoginRequest(user.Email, password));

        //Assert
        response.Should().NotBeNull();
        response.Name.Should().Be(user.Name);
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ExecuteAsync_WhenPasswordIsInvalid_ShouldThrowError()
    {
        //Arrange
        (var user, _) = UserBuilder.Build();
        _userRepository.Setup(x => x.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _tokenService.Setup(x => x.GenerateToken(user)).Returns("ABCD");

        //Act
        Func<Task> action = async () => 
        { 
            await _loginUseCase.ExecuteAsync(new LoginRequest(user.Email, "invalidPassword")); 
        };

        //Assert
        await action.Should().ThrowAsync<InvalidLoginException>()
            .Where(exception => exception.ErrorMessages.Contains(ErrorMessages.InvalidLogin));
    }

    [Fact]
    public async Task ExecuteAsync_WhenEmailIsNotFound_ShouldThrowError()
    {
        //Arrange
        (var user, var password) = UserBuilder.Build();
        _userRepository.Setup(x => x.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _tokenService.Setup(x => x.GenerateToken(user)).Returns("ABCD");

        //Act
        Func<Task> action = async () =>
        {
            await _loginUseCase.ExecuteAsync(new LoginRequest("notfound@email.com", password));
        };

        //Assert
        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Contains("User not found"));
    }

    [Fact]
    public async Task ExecuteAsync_WhenEmailAndPasswordAreInvalid_ShouldThrowError()
    {
        //Arrange
        (var user, _) = UserBuilder.Build();
        _userRepository.Setup(x => x.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _tokenService.Setup(x => x.GenerateToken(user)).Returns("ABCD");

        //Act
        Func<Task> action = async () =>
        {
            await _loginUseCase.ExecuteAsync(new LoginRequest("notfound@email.com", "invalidPassword"));
        };

        //Assert
        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Contains("User not found"));
    }



}
