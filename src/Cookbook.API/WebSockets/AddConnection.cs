using Cookbook.Application.UseCases.Connection;
using Cookbook.Application.UseCases.Connection.QrCodeRead;
using Cookbook.Communication.Response;
using Cookbook.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Cookbook.API.WebSockets;

[Authorize(Policy = "AuthenticatedUser")]
public class AddConnection : Hub
{
    private readonly Broadcaster _broadcaster;
    private readonly IGenerateQrCodeUseCase _generateQrCodeUseCase;
    private readonly IHubContext<AddConnection> _hubContext;
    private readonly IQRCodeReadUseCase _qRCodeReadUseCase;

    public AddConnection(IGenerateQrCodeUseCase generateQrCodeUseCase, IHubContext<AddConnection> hubContext, IQRCodeReadUseCase qRCodeReadUseCase)
    {
        _broadcaster = Broadcaster.Instance;
        _generateQrCodeUseCase = generateQrCodeUseCase;
        _hubContext = hubContext;
        _qRCodeReadUseCase = qRCodeReadUseCase;
    }

    public async Task GetQrCodeAsync()
    {
        (var qrCode, var userId) = await _generateQrCodeUseCase.ExecuteAsync();

        _broadcaster.InitializeConnection(_hubContext, userId, Context.ConnectionId);

        //send to all
        //await Clients.All.SendAsync("ResultQrCode", qrCode);

        //to a specific connection
        //await Clients.Clients(Context.ConnectionId).SendAsync("ResultQrCode", qrCode);

        //to specific group
        //await Groups.AddToGroupAsync(Context.ConnectionId, "groupXYZ");
        //await Clients.Group("groupXYZ").SendAsync("ResultQrCode", qrCode);

        //to whoever called this function
        await Clients.Caller.SendAsync("ResultQrCode", qrCode);
    }

    public async Task QRCodeReadASync(string connectionCode)
    {
        try
        {
            (var userToConnect, var ownerQrCdeId) = await _qRCodeReadUseCase.ExecuteAsync(connectionCode);

            var connectionId = _broadcaster.GetUserConnectionId(ownerQrCdeId);

            await Clients.Client(connectionId).SendAsync("ResultQrCodeRead", userToConnect.Name);
        }
        catch (InvalidOperationException ex)
        {
            await Clients.Caller.SendAsync("Error", ex.Message);
        }
        catch(Exception ex)
        {
            await Clients.Caller.SendAsync("Error", ErrorMessages.UnknownError);
        }
    }
}
