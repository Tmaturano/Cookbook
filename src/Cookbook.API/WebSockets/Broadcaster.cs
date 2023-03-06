using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Cookbook.API.WebSockets;

/// <summary>
/// Share information between active connections
/// </summary>
public class Broadcaster
{
    private readonly static Lazy<Broadcaster> _instance = new(() => new Broadcaster());

    public static Broadcaster Instance {  get { return _instance.Value; } }

    private ConcurrentDictionary<string, object> _dictionary { get; set; }

    public Broadcaster() => _dictionary = new ConcurrentDictionary<string, object>();

    public void InitializeConnection(IHubContext<AddConnection> hubContext, string ownerQrCdeId, string connectionId)
    {
        var connection = new Connection(hubContext, connectionId);
        _dictionary.TryAdd(connectionId, connection);
        _dictionary.TryAdd(ownerQrCdeId, connectionId);

        connection.InitializeTimeCount(CallbackExpiredTime);
    }

    public string GetUserConnectionId(string userId)
    {
        if (!_dictionary.TryGetValue(userId, out var connectionId))
            throw new InvalidOperationException("");

        return connectionId.ToString();
    }

    public void ResetExpireTime(string connectionId)
    {
        _dictionary.TryGetValue(connectionId, out var connectionObject);
        
        var connection = connectionObject as Connection;
        connection?.ResetTimer();
    }

    public string Remove(string connectionId, string ownerQrCodeId)
    {
        _dictionary.TryGetValue(connectionId, out var connectionObject);

        var connection = connectionObject as Connection;
        connection?.StopTimer();

        _dictionary.TryRemove(connectionId, out _);
        _dictionary.TryRemove(ownerQrCodeId, out _);

        return connection.ReadQrCodeUser();
    }

    public void SetQrCodeUserConnectionId(string userIdQrCodeRead, string qrCodeReadUserConnectionId)
    {
        var qrCodeReadConnectionId = GetUserConnectionId(userIdQrCodeRead);  

        _dictionary.TryGetValue(qrCodeReadConnectionId, out var connectionObject);
        var connection = connectionObject as Connection;

        connection?.SetQrCodeUserConnectionId(qrCodeReadUserConnectionId);
    }

    private void CallbackExpiredTime(string connectionId) => _dictionary.TryRemove(connectionId, out _);
}
