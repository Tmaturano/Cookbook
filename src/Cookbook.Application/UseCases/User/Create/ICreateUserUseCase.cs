using Cookbook.Communication.Request;
using Cookbook.Communication.Response;

namespace Cookbook.Application.UseCases.User.Create;

public interface ICreateUserUseCase
{
    Task<UserRegisteredResponse> ExecuteAsync(RegisterUserRequest request);
}
