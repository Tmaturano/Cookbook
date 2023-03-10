namespace Cookbook.Application.UseCases.Connection.AcceptConnection;

public interface IAcceptConnectionUseCase
{
    Task<string> ExecuteAsync(Guid userToConnectId);
}
