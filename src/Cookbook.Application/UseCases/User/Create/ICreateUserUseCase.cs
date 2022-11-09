using Cookbook.Communication.Request;

namespace Cookbook.Application.UseCases.User.Create;

public interface ICreateUserUseCase
{
    Task ExecuteAsync(RegisterUserRequest request);
}
