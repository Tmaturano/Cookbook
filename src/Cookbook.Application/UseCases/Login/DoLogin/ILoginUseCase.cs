using Cookbook.Communication.Request;
using Cookbook.Communication.Response;

namespace Cookbook.Application.UseCases.Login.DoLogin;

public interface ILoginUseCase
{
    Task<LoginResponse> ExecuteAsync(LoginRequest request);
}
