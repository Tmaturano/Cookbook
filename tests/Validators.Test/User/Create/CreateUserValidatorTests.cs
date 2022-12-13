using Cookbook.Application.UseCases.User.Create;
using Cookbook.Communication.Request;
using Cookbook.Exceptions;
using FluentAssertions;
using Helper.Requests;
using Xunit;

namespace Validators.Test.User.Create;

public class CreateUserValidatorTests
{
    [Fact]
    public void Validator_Success()
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();        
    }

    [Fact]
    public void Validator_WhenNameIsEmpty_ShouldReturnError()
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        var requestWithEmptyName = request with { Name = string.Empty };

        //Act
        var result = validator.Validate(requestWithEmptyName);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.EmptyUserName));
    }

    [Fact]
    public void Validator_WhenEmailIsEmpty_ShouldReturnError()
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        var requestWithEmptyName = request with { Email = string.Empty };

        //Act
        var result = validator.Validate(requestWithEmptyName);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.EmptyUserEmail));
    }

    [Fact]
    public void Validator_WhenPasswordIsEmpty_ShouldReturnError()
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        var requestWithEmptyName = request with { Password = string.Empty };

        //Act
        var result = validator.Validate(requestWithEmptyName);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.EmptyUserPassword));
    }

    [Fact]
    public void Validator_WhenPhoneIsEmpty_ShouldReturnError()
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        var requestWithEmptyName = request with { Phone = string.Empty };

        //Act
        var result = validator.Validate(requestWithEmptyName);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.EmptyUserPhone));
    }

    [Fact]
    public void Validator_WhenEmailIsInvalid_ShouldReturnError()
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        var requestWithEmptyName = request with { Email = "invalid" };

        //Act
        var result = validator.Validate(requestWithEmptyName);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.InvalidUserEmail));
    }

    [Fact]
    public void Validator_WhenPhoneIsInvalid_ShouldReturnError()
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        var requestWithEmptyName = request with { Phone = "123 456" };

        //Act
        var result = validator.Validate(requestWithEmptyName);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.InvalidUserPhone));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validator_WhenPasswordLengthIsLessThan6Characters_ShouldReturnError(int passwordLength)
    {
        //Arrange
        var validator = new CreateUserValidator();
        var request = RegisterUserRequestBuilder.Build(passwordLength);

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.InvalidUserPassword));
    }
}
