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

    private ConcurrentDictionary<string, Connection> _dictionary { get; set; }

    public Broadcaster() => _dictionary = new ConcurrentDictionary<string, Connection>();

    public void InitializeConnection(IHubContext<AddConnection> hubContext, string connectionId)
    {
        var connection = new Connection(hubContext, connectionId);
        _dictionary.TryAdd(connectionId, connection);

        connection.InitializeTimeCount(CallbackExpiredTime);
    }

    private void CallbackExpiredTime(string connectionId) => _dictionary.TryRemove(connectionId, out _);
}
