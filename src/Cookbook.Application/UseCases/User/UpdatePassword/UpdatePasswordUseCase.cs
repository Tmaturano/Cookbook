using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Communication.Request;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;
using Cookbook.Exceptions;
using Cookbook.Exceptions.ExceptionsBase;
using FluentValidation.Results;
using SecureIdentity.Password;

namespace Cookbook.Application.UseCases.User.UpdatePassword;

public class UpdatePasswordUseCase : IUpdatePasswordUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticatedUser _authenticatedUser;

    public UpdatePasswordUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthenticatedUser authenticatedUser)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authenticatedUser = authenticatedUser;
    }
    public async Task ExecuteAsync(UpdatePasswordRequest request)
    {
        var authenticatedUser = await _authenticatedUser.GetAsync();

        var user = await _userRepository.GetByIdAsync(authenticatedUser.Id);

        await ValidateAsync(request, user);

        user.SetPassword(PasswordHasher.Hash(request.NewPassword));

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();
    }

    private async Task ValidateAsync(UpdatePasswordRequest request, Domain.Entities.User user)
    {
        var validator = new UpdatePasswordValidator();
        var result = await validator.ValidateAsync(request);

        var encryptedCurrentPassword = PasswordHasher.Hash(request.CurrentPassword);

        if (!user.PasswordHash.Equals(encryptedCurrentPassword))
            result.Errors.Add(new ValidationFailure("currentPassword", ErrorMessages.InvalidCurrentPassword));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
