namespace Cookbook.Application.UseCases.Connection.RefuseConnection;

public interface IRefuseConnectionUseCase
{
    Task<Guid> ExecuteAsync();    
}
