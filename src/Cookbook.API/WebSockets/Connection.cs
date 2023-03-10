using Microsoft.AspNetCore.SignalR;

namespace Cookbook.API.WebSockets;

public class Connection
{
    private readonly IHubContext<AddConnection> _hubContext;
    private readonly string _ownerConnectionId;
    private Action<string> _callbackExpiredTime;
    private string _ownerQrCodeConnectionId; 


    public Connection(IHubContext<AddConnection> hubContext, string ownerConnectionId)
    {
        _hubContext = hubContext;
        _ownerConnectionId = ownerConnectionId;
    }

    private short _timeLeftInSeconds { get; set; }
    private System.Timers.Timer _timer { get; set; }

    public void InitializeTimeCount(Action<string> callbackExpiredTime)
    {
        _callbackExpiredTime = callbackExpiredTime;

        StartTimer();
    }

    public void ResetTimer()
    {
        StopTimer();
        StartTimer();
    }

    public void StopTimer()
    {
        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;
    }

    public void SetQrCodeUserConnectionId(string connectionId) => _ownerQrCodeConnectionId = connectionId;

    public string ReadQrCodeUser() => _ownerQrCodeConnectionId;

    private void StartTimer()
    {
        _timeLeftInSeconds = 60;
        _timer = new System.Timers.Timer(1000)
        {
            Enabled = false
        };
        _timer.Elapsed += ElapsedTimer;
        _timer.Enabled = true;
    }

    private async void ElapsedTimer(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (_timeLeftInSeconds >= 0)
            await _hubContext.Clients.Client(_ownerConnectionId).SendAsync("SetTimeLeft", _timeLeftInSeconds--);
        else
        {
            StopTimer();
            _callbackExpiredTime(_ownerConnectionId);
        }
    }
}
