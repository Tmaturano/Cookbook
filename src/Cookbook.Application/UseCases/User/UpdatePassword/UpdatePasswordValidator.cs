using Cookbook.Communication.Request;
using FluentValidation;

namespace Cookbook.Application.UseCases.User.UpdatePassword;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequest>
{
    public UpdatePasswordValidator() => RuleFor(c => c.NewPassword).SetValidator(new PasswordValidator());
}
