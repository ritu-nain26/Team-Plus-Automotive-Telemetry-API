namespace Team_Plus_Automotive_Telemetry_API.Models.Dashboard
{
    public class VehicleParameter
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }

    public class DashboardModel
    {
        public List<VehicleParameter> Parameters { get; set; }
    }
}
