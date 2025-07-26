using RealTimeDashboard.Classes;
using System.Collections.Concurrent;
using System.Text.Json;

public class UpdateHandler : IMessageActionHandler
{
    public async Task HandleAsync(JsonElement payload, ConcurrentDictionary<Guid, Item> items)
    {
        var updatedData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(payload.GetRawText(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (updatedData != null && updatedData.TryGetValue("id", out var idElement) &&
            Guid.TryParse(idElement.GetString(), out Guid id) &&
            items.TryGetValue(id, out var existingItem))
        {
            if (updatedData.TryGetValue("name", out var nameEl))
                existingItem.Name = nameEl.GetString();

            if (updatedData.TryGetValue("price", out var priceEl))
                existingItem.Price = priceEl.GetDouble();

            if (updatedData.TryGetValue("quantity", out var qtyEl))
                existingItem.Quantity = qtyEl.GetInt32();

        }

        await Task.CompletedTask;
    }
}

