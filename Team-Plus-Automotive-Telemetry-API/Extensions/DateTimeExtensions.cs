namespace Team_Plus_Automotive_Telemetry_API.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            // Ensure the DateTime is in UTC
            DateTime utcDateTime = dateTime.ToUniversalTime();

            // Define the reference date (2024-01-01)
            DateTime referenceDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Calculate the Unix timestamp in milliseconds
            long unixTimestamp = (long)(utcDateTime - referenceDate).TotalMilliseconds;

            return unixTimestamp;
        }
    }
}
