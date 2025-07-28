using RealTimeDashboard.Models;
using System.Collections.Concurrent;
using System.Text.Json;

public class InsertHandler : IMessageActionHandler
{
    public async Task HandleAsync(JsonElement payload, ConcurrentDictionary<Guid, Item> items)
    {
        var newItem = payload.Deserialize<Item>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (newItem != null && !string.IsNullOrWhiteSpace(newItem.Name) && newItem.Price > 0)
        {
            newItem.Id = Guid.NewGuid();
            items[newItem.Id] = newItem;
        }
        await Task.CompletedTask;
    }
}
