namespace Team_Plus_Automotive_Telemetry_API.Models.Data.Push
{
    public class PushDataRequest
    {
        public string DeviceId { get; set; }
        public long Timestamp { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public PushDataRequest()
        {
            Parameters = new Dictionary<string, string>();
        }
    }
}
