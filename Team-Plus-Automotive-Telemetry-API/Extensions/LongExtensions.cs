namespace Team_Plus_Automotive_Telemetry_API.Utility
{
    public static class LongExtensions
    {
        public static DateTime ToDateTime(this long timestamp)
        {
            // Define the reference date (2024-01-01)
            DateTime referenceDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Add the timestamp (milliseconds) to the reference date
            DateTime dateTime = referenceDate.AddMilliseconds(timestamp);

            return dateTime;
        }
        public static bool IsValid(long timestamp)
        {
            try
            {
                timestamp.ToDateTime();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
