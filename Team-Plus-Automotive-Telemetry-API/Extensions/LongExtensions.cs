namespace Team_Plus_Automotive_Telemetry_API.Utility
{
    public static class LongExtensions
    {
        public static DateTime ToDateTime(this long timestamp)
        {
            // Define the base date (for example, a fixed epoch date)
            DateTime baseDate = new DateTime(2024, 1, 1); // Replace with your base date if known

            // Convert milliseconds to TimeSpan
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(timestamp);

            // Add TimeSpan to base date
            DateTime dateTime = baseDate.Add(timeSpan);

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
