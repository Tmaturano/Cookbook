using Cookbook.Communication.Request;
using Cookbook.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Cookbook.Application.UseCases.User.Create;
public class CreateUserValidator : AbstractValidator<RegisterUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(ErrorMessages.EmptyUserName);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ErrorMessages.EmptyUserEmail);
        RuleFor(c => c.Password).NotEmpty().WithMessage(ErrorMessages.EmptyUserPassword);
        RuleFor(c => c.Phone).NotEmpty().WithMessage(ErrorMessages.EmptyUserPhone);

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ErrorMessages.InvalidUserEmail);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Password), () =>
        {
            RuleFor(c => c.Password).MinimumLength(6).WithMessage(ErrorMessages.InvalidUserPassword);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Phone), () =>
        {
            RuleFor(c => c.Phone).Custom((phone, context) =>
            {
                var phonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(phone, phonePattern);                
                if (!isMatch)
                    context.AddFailure(ErrorMessages.InvalidUserPhone);
            });
        });
    }
}
