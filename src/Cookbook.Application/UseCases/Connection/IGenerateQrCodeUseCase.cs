namespace Cookbook.Application.UseCases.Connection;

public interface IGenerateQrCodeUseCase
{
    Task<(string QrCode, string UserId)> ExecuteAsync();
}
