using Cookbook.Communication.Request;

namespace Cookbook.Application.UseCases.User.UpdatePassword;

public interface IUpdatePasswordUseCase
{
    Task ExecuteAsync(UpdatePasswordRequest request);
}
