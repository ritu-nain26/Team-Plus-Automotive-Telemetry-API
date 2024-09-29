namespace Team_Plus_Automotive_Telemetry_API.Models.Common
{
    public static class VehicleParameterMapping
    {
        public static Dictionary<string, string> Map = new Dictionary<string, string>
        {
            {"104", "Engine load"},
            {"105", "Engine coolant temperature"},
            {"10a", "Fuel pressure"},
            {"10b", "Intake manifold absolute pressure"},
            {"10c", "Engine RPM"},
            {"10d", "Vehicle speed"},
            {"10e", "Timing advance"},
            {"10f", "Intake air temperature"},
            {"110", "MAF air flow rate"},
            {"111", "Throttle position"},
            {"11f", "Run time since engine start"},
            {"121", "Distance traveled with malfunction indicator lamp"},
            {"12f", "Fuel Level Input"},
            {"131", "Distance traveled since codes cleared"},
            {"133", "Barometric pressure"},
            {"142", "Control module voltage"},
            {"143", "Absolute load value"},
            {"15b", "Hybrid battery pack remaining life"},
            {"15c", "Engine oil temperature"},
            {"15e", "Engine fuel rate"},
            // Additional Mappings
            {"11", "UTC Date (DDMMYY)"},
            {"10", "UTC Time (HHMMSSmm)"},
            {"A", "Latitude"},
            {"B", "Longitude"},
            {"C", "Altitude (m)"},
            {"D", "Speed (km/h)"},
            {"E", "Course (degree)"},
            {"F", "Number of satellites in use"},
            {"20", "Accelerometer data (x:y:z)"},
            {"21", "Gyroscope data (x:y:z)"},
            {"22", "Magnitude field data (x/y/z)"},
            {"23", "MEMS temperature (in 0.1 Celsius degree)"},
            {"24", "Battery voltage (in 0.01V)"},
            {"25", "Orientation data (yaw/pitch/roll)"},
            {"81", "Cellular network signal level (dB)"},
            {"82", "CPU temperature (in 0.1 Celsius degree)"},
            {"83", "CPU hall sensor data"}
        };

    }
}
