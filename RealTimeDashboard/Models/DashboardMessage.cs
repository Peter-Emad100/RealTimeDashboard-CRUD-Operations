using System.Text.Json;

namespace RealTimeDashboard.Models
{
    public class DashboardMessage
    {
        public string Action { get; set; }
        public JsonElement Payload { get; set; }
    }
}
