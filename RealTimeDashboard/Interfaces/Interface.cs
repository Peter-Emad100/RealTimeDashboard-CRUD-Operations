using RealTimeDashboard.Classes;
using System.Collections.Concurrent;
using System.Text.Json;

public interface IMessageActionHandler
{
    Task HandleAsync(JsonElement payload, ConcurrentDictionary<Guid, Item> items);
}

