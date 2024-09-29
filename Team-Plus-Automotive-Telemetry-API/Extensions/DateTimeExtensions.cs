namespace Team_Plus_Automotive_Telemetry_API.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            // Ensure the DateTime is in UTC
            DateTime utcDateTime = dateTime.ToUniversalTime();

            // Calculate the Unix timestamp (in seconds)
            long unixTimestamp = (long)(utcDateTime - new DateTime(2024, 1, 1)).TotalSeconds;

            return unixTimestamp;
        }
    }
}
