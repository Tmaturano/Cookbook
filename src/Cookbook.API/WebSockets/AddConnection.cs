using Cookbook.Application.UseCases.Connection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Cookbook.API.WebSockets;

[Authorize(Policy = "AuthenticatedUser")]
public class AddConnection : Hub
{
    private readonly Broadcaster _broadcaster;
    private readonly IGenerateQrCodeUseCase _generateQrCodeUseCase;
    private readonly IHubContext<AddConnection> _hubContext;

    public AddConnection(IGenerateQrCodeUseCase generateQrCodeUseCase, IHubContext<AddConnection> hubContext)
    {
        _broadcaster = Broadcaster.Instance;

        _generateQrCodeUseCase = generateQrCodeUseCase;
        _hubContext = hubContext;
    }

    public async Task GetQrCodeAsync()
    {
        var qrCode = await _generateQrCodeUseCase.ExecuteAsync();

        _broadcaster.InitializeConnection(_hubContext, Context.ConnectionId);

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
}
