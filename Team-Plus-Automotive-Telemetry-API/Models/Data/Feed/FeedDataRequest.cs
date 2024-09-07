namespace Team_Plus_Automotive_Telemetry_API.Models.Data.Feed
{
    public class FeedDataRequest
    {
        public string DeviceId { get; set; }
        public long Timestamp { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public FeedDataRequest()
        {
            Parameters = new Dictionary<string, string>();
        }
    }
}
