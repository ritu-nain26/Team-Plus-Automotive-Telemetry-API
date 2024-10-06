// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


// Define the original DateTime
DateTime originalDateTime = DateTime.Now;
Console.WriteLine("Original DateTime: " + originalDateTime);

// Convert DateTime to timestamp
long timestamp = (originalDateTime).ToTimestamp();
Console.WriteLine("Timestamp (milliseconds): " + timestamp);

// Convert timestamp back to DateTime
DateTime convertedDateTime = (timestamp).ToDateTime();
Console.WriteLine("Converted DateTime: " + convertedDateTime);


Console.WriteLine();

public static class DateTimeExtensions
{
    // Method to convert DateTime to timestamp (milliseconds since 2024-01-01)
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

    // Method to convert timestamp back to DateTime
    public static DateTime ToDateTime(this long timestamp)
    {
        // Define the reference date (2024-01-01)
        DateTime referenceDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Add the timestamp (milliseconds) to the reference date
        DateTime dateTime = referenceDate.AddMilliseconds(timestamp);

        return dateTime;
    }
}


//Time stamp : 24235521 Date time now : 01-Jan-24 6:43:55 AM
//Date time now : 07 - Oct - 24 11:05:21 PMTime stamp : 24235521