using Cookbook.Communication.Request;
using Cookbook.Exceptions;
using FluentValidation;

namespace Cookbook.Application.UseCases.Login.DoLogin;

public class LoginValidator : AbstractValidator<LoginRequest>
{
	public LoginValidator()
	{
        RuleFor(c => c.Email).NotEmpty().WithMessage(ErrorMessages.EmptyUserEmail);
        RuleFor(c => c.Password).NotEmpty().WithMessage(ErrorMessages.EmptyUserPassword);
    }
}
