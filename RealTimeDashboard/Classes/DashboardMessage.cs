using System.Text.Json;

namespace RealTimeDashboard.Classes
{
    public class DashboardMessage
    {
        public string Action { get; set; }
        public JsonElement Payload { get; set; }
    }
}
