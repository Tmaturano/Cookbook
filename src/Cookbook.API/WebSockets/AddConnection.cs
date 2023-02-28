using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Cookbook.API.WebSockets;

[Authorize(Policy = "AuthenticatedUser")]
public class AddConnection : Hub
{
    public async Task GetQrCodeAsync()
    {
        var qrCode = "ABCD123";

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

    public override Task OnConnectedAsync()
    {
        var context = Context.ConnectionId;

        return base.OnConnectedAsync();
    }
}
