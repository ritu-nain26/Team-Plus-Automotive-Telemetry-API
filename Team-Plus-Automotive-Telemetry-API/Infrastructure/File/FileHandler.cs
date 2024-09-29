using System.Globalization;
using Team_Plus_Automotive_Telemetry_API.Extensions;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Feed;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch;
using Team_Plus_Automotive_Telemetry_API.Utility;

namespace Team_Plus_Automotive_Telemetry_API.Infrastructure.File
{
    public class FileHandler : IFileHandler
    {
        private int _successCounter = 0;
        private readonly string parentFolder = "Data";
        public int Write(FeedDataRequest feedData)
        {
            var filePath = GetFilePath(feedData.DeviceId, feedData.Timestamp);

            if (feedData?.Parameters.Count > 0)
            {
                // Convert the dictionary to a string in the format "key:value"
                string line = string.Join(",", feedData.Parameters.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
                _successCounter = feedData.Parameters.Count;

                // If the file exists and has content, append a new line followed by the new data
                System.IO.File.AppendAllText(filePath, Environment.NewLine + line);

            }

            return _successCounter;
        }
        public List<string> Read(FetchDataRequest fetchData)
        {
            var lines = new List<string>();
            var filePath = GetFilePath(fetchData.DeviceId, fetchData.Timestamp);

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            lines.Add(line);
                        }
                    }
                    Console.WriteLine("File read successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while reading the file: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
            }

            return lines;
        }
        public void CreateFile(string deviceId, long timeStamp)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            // Create root directory
            if (!Directory.Exists(Path.Combine(currentDirectory, parentFolder)))
            {
                Directory.CreateDirectory(Path.Combine(currentDirectory, parentFolder));
            }

            var dateTime = timeStamp.ToDateTime();

            var subFolder = $"{deviceId}/{dateTime.Date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)}";
            // Combine paths
            string subFolderPath = Path.Combine(currentDirectory, parentFolder, subFolder);
            // Create root directory
            if (!Directory.Exists(subFolderPath))
            {
                Directory.CreateDirectory(subFolderPath);
            }

            // create file now

            var filePath = Path.Combine(subFolderPath, $"{dateTime.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture)}.csv");

            if (System.IO.File.Exists(filePath))
            {
                // Read the file line by line
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("TripEnd", StringComparison.OrdinalIgnoreCase)) // Case-insensitive search
                        {
                            throw new Exception($"Feed ended for deviceId {deviceId} and timeStamp {timeStamp}, start a new feed");
                        }
                    }
                }
            }
            else
            {
                // Create an empty file
                var line = $"TripStart:{timeStamp}";
                System.IO.File.AppendAllText(filePath, line);
            }
        }
        public void CloseFeed(string deviceId, long timeStamp)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            // Create root directory
            if (!Directory.Exists(Path.Combine(currentDirectory, parentFolder)))
            {
                throw new Exception($"Can not find feed for deviceId {deviceId} and timeStamp {timeStamp}");
            }

            var dateTime = timeStamp.ToDateTime();

            var subFolder = $"{deviceId}/{dateTime.Date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)}";
            // Combine paths
            string subFolderPath = Path.Combine(currentDirectory, parentFolder, subFolder);
            // Create root directory
            if (!Directory.Exists(subFolderPath))
            {
                throw new Exception($"Can not find feed for deviceId {deviceId} and timeStamp {timeStamp}");
            }
            var filePath = Path.Combine(subFolderPath, $"{dateTime.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture)}.csv");

            if (System.IO.File.Exists(filePath))
            {
                // Read the file line by line
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("TripEnd", StringComparison.OrdinalIgnoreCase)) // Case-insensitive search
                        {
                            throw new Exception($"Feed ended for deviceId {deviceId} and timeStamp {timeStamp}, start a new feed");
                        }
                    }
                }

                var endTrip = $"TripEnd:{DateTime.Now.ToTimestamp()}";

                System.IO.File.AppendAllText(filePath, Environment.NewLine + endTrip);
            }
            else
            {
                throw new Exception($"Can not find feed for deviceId {deviceId} and timeStamp {timeStamp}");
            }
        }
        private string GetFilePath(string deviceId, long TS)
        {
            // Define the base date (for example, a fixed epoch date)
            DateTime baseDate = new DateTime(2024, 1, 1); // Replace with your base date if known

            // Convert milliseconds to TimeSpan
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(TS);

            // Add TimeSpan to base date
            DateTime dateTime = baseDate.Add(timeSpan);

            var subFolder = $"{deviceId}/{dateTime.Date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)}";
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), parentFolder, subFolder);
            var fileName = $"{dateTime.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture)}";
            var filePath = Path.Combine(directoryPath, fileName + ".csv");

            return filePath;
        }
    }
}
