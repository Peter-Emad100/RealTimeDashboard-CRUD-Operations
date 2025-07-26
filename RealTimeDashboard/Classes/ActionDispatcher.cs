using RealTimeDashboard.Classes;
using System.Collections.Concurrent;
using System.Text.Json;

public class ActionDispatcher
{
    private readonly Dictionary<string, IMessageActionHandler> _handlers = new();

    public void Register(string action, IMessageActionHandler handler)
    {
        _handlers[action.ToLower()] = handler;
    }

    public async Task DispatchAsync(string action, JsonElement payload, ConcurrentDictionary<Guid, Item> items)
    {
        if (_handlers.TryGetValue(action.ToLower(), out var handler))
        {
            await handler.HandleAsync(payload, items);
        }
        else
        {
            Console.WriteLine($"Unknown action: {action}");
        }
    }
}

