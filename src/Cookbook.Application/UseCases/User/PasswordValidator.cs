using Cookbook.Exceptions;
using FluentValidation;

namespace Cookbook.Application.UseCases.User;

public class PasswordValidator : AbstractValidator<string>
{
	public PasswordValidator()
	{
        RuleFor(password => password).NotEmpty().WithMessage(ErrorMessages.EmptyUserPassword);

        When(password => !string.IsNullOrWhiteSpace(password), () =>
        {
            RuleFor(c => c).MinimumLength(6).WithMessage(ErrorMessages.InvalidUserPassword);
        });
    }
}
