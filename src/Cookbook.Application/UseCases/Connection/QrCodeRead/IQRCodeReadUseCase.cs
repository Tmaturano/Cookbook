using Cookbook.Communication.Response;

namespace Cookbook.Application.UseCases.Connection.QrCodeRead;

public interface IQRCodeReadUseCase
{
    Task<(UserConnectionResponse UserToConnect, string OwnerQrCdeId)> ExecuteAsync(string connectionCode);
}
