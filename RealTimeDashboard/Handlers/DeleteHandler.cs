using RealTimeDashboard.Models;
using System.Collections.Concurrent;
using System.Text.Json;

public class DeleteHandler : IMessageActionHandler
{
    public async Task HandleAsync(JsonElement payload, ConcurrentDictionary<Guid, Item> items)
    {
        var deleteData = JsonSerializer.Deserialize<Dictionary<string, string>>(payload.GetRawText(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (deleteData != null &&
            deleteData.TryGetValue("id", out var idStr) &&
            Guid.TryParse(idStr, out Guid id))
        {
            items.TryRemove(id, out _);
        }

        await Task.CompletedTask;
    }
}

