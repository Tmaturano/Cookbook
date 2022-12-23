using Cookbook.Application.UseCases.User.Create;
using Cookbook.Application.UseCases.User.UpdatePassword;
using Cookbook.Exceptions;
using FluentAssertions;
using Helper.Requests;
using Xunit;

namespace Validators.Test.User.UpdatePassword;

public class UpdatePasswordValidatorTests
{
    [Fact]
    public void Validator_Success()
    {
        //Arrange
        var validator = new UpdatePasswordValidator();
        var request = UpdatePasswordRequestBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
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
        var validator = new UpdatePasswordValidator();
        var request = UpdatePasswordRequestBuilder.Build(passwordLength);

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.InvalidUserPassword));
    }

    [Fact]
    public void Validator_WhenNewPasswordIsEmpty_ShouldReturnError()
    {
        //Arrange
        var validator = new UpdatePasswordValidator();
        var request = UpdatePasswordRequestBuilder.Build();
        var requestWithEmptyName = request with { NewPassword = string.Empty };

        //Act
        var result = validator.Validate(requestWithEmptyName);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ErrorMessages.EmptyUserPassword));
    }
}
