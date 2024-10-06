namespace Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch
{
    public class FetchDataResponse
    {
        public Stats Stats { get; set; }
        public List<List<object>> Data { get; set; }
    }

    public class Stats
    {
        public long Tick { get; set; }
        public long DevTick { get; set; }
        public int Elapsed { get; set; }
        public Age Age { get; set; }
        public int Parked { get; set; }
    }

    public class Age
    {
        public int Data { get; set; }
        public int Ping { get; set; }
    }
}
