using Microsoft.AspNetCore.SignalR;
using RealTimeDashboard.Classes;
using System.Text.Json;

namespace RealTimeDashboard.Hubs
{
    public class DashboardHub : Hub
    {
        private static Dictionary<Guid, Item> items = new Dictionary<Guid, Item>();

        public async Task DashboardAction(string messageJson)
        {
            Console.WriteLine(messageJson);
            var msg = JsonSerializer.Deserialize<DashboardMessage>(messageJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Console.WriteLine(msg);
            Console.WriteLine(msg.Action);
            Console.WriteLine(msg.Payload);
            switch (msg.Action)
            {
                case "insert":
                    var newItem = msg.Payload.Deserialize<Item>(new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    newItem.Id = Guid.NewGuid();
                    items[newItem.Id] = newItem;
                    break;

                case "update":
                    var updatedData = msg.Payload.Deserialize<Dictionary<string, JsonElement>>(new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var updateId = Guid.Parse(updatedData["id"].GetString());

                    if (items.ContainsKey(updateId))
                    {
                        if (updatedData.ContainsKey("name"))
                            items[updateId].Name = updatedData["name"].GetString();

                        if (updatedData.ContainsKey("price"))
                            items[updateId].Price = updatedData["price"].GetDouble();

                        if (updatedData.ContainsKey("quantity"))
                            items[updateId].Quantity = updatedData["quantity"].GetInt32();
                    }
                    break;

                case "delete":
                    var deleteData = msg.Payload.Deserialize<Dictionary<string, string>>(new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var deleteId = Guid.Parse(deleteData["id"]);
                    items.Remove(deleteId);
                    break;

                case "clear":
                    items.Clear();
                    break;
            }

            // Broadcast the updated list to all clients
            await Clients.All.SendAsync("ReceiveItems", items.Values);
        }

    }

}
