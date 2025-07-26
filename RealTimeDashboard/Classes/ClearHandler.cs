using RealTimeDashboard.Classes;
using System.Collections.Concurrent;
using System.Text.Json;

public class ClearHandler : IMessageActionHandler
{
    public async Task HandleAsync(JsonElement payload, ConcurrentDictionary<Guid, Item> items)
    { items.Clear();
      await Task.CompletedTask;
    }
}

