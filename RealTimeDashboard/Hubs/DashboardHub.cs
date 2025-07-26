using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using RealTimeDashboard.Classes;
using System.Collections.Concurrent;
using System.Text.Json;

namespace RealTimeDashboard.Hubs
{
    public class DashboardHub : Hub
    {
        private static ConcurrentDictionary<Guid, Item> items = new ConcurrentDictionary<Guid, Item>();

        public async Task DashboardAction(string messageJson)
        {
            var msg = JsonSerializer.Deserialize<DashboardMessage>(messageJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var dispatcher = new ActionDispatcher();
            dispatcher.Register("insert", new InsertHandler());
            dispatcher.Register("update", new UpdateHandler());
            dispatcher.Register("delete", new DeleteHandler());
            dispatcher.Register("clear", new ClearHandler());
            await dispatcher.DispatchAsync(msg.Action, msg.Payload, items);

            // Broadcast the updated list to all clients
            await Clients.All.SendAsync("ReceiveItems", items.Values);
        }

    }

}
