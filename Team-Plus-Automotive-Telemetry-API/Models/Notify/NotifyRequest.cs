using Team_Plus_Automotive_Telemetry_API.Models.Common;

namespace Team_Plus_Automotive_Telemetry_API.Models.Notify
{
    public class NotifyRequest
    {
        public string VIN { get; set; }
        public string DeviceId { get; set; }
        public long TimeStamp { get; set; }
        public EventEnum Event { get; set; }
    }
}
