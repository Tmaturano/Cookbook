namespace Cookbook.Application.UseCases.Connection;

public interface IGenerateQrCodeUseCase
{
    Task<string> ExecuteAsync();
}
