using Team_Plus_Automotive_Telemetry_API.Models.Data.Pull;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Push;

namespace Team_Plus_Automotive_Telemetry_API.Infrastructure.File
{
    public class FileHandler : IFileHandler
    {
        //private string _filePath = "./Data";
        private int _successCounter = 0;
        public int Write(PushDataRequest pushData)
        {
            // Construct the file name and path
            string fileName = $"{pushData.DeviceId}_{pushData.Timestamp}";
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            var filePath = Path.Combine(directoryPath, fileName + ".csv");

            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (pushData?.Parameters.Count > 0)
            {
                // Convert the dictionary to a string in the format "key:value"
                string line = string.Join(",", pushData.Parameters.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
                _successCounter = pushData.Parameters.Count;

                // Check if the file exists and if it has content
                if (System.IO.File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                {
                    // If the file exists and has content, append a new line followed by the new data
                    System.IO.File.AppendAllText(filePath, Environment.NewLine + line);
                }
                else
                {
                    // If the file does not exist or is empty, write the line normally
                    System.IO.File.AppendAllText(filePath, line);
                }
            }
            return _successCounter;
        }

        public List<string> Read(PullDataRequest request)
        {
            var lines = new List<string>();
            string fileName = $"{request.DeviceId}_{request.Timestamp}";
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            var filePath = Path.Combine(directoryPath, fileName + ".csv");

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
    }
}
