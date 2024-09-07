namespace Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch
{
    public class FetchDataStats
    {
        public string Tick { get; set; }
        public string Devtick { get; set; }
        public string Elapsed { get; set; }
        public string Parked { get; set; }
        public FetchDataAge Age { get; set; }
    }

    public class FetchDataAge
    {
        public string Data { get; set; }
        public string Ping { get; set; }
    }
}
