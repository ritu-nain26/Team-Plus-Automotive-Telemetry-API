using Team_Plus_Automotive_Telemetry_API.Models.Common;

namespace Team_Plus_Automotive_Telemetry_API.Models.Login
{
    public class LoginRequest
    {
        public string DeviceID { get; set; }
        public EventEnum Event { get; set; }
        public DateTime CurrentTimestamp { get; set; }
        public string VIN { get; set; }
        public string Checksum { get; set; }
    }
}
