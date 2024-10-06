using Team_Plus_Automotive_Telemetry_API.Extensions;
using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch;
using Team_Plus_Automotive_Telemetry_API.Utility;

namespace Team_Plus_Automotive_Telemetry_API.Handlers.Data
{
    public class FetchDataHandler : IHandler<FetchDataRequest, FetchDataResponse>
    {
        private readonly IFileHandler fileHandler;
        long? deviceTimeStamp = DateTime.Now.ToTimestamp();
        public FetchDataHandler(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }
        public FetchDataResponse Handle(FetchDataRequest request)
        {
            var result = new List<List<object>>();
            var stat = new Stats();
            stat.Parked = 1;
            int elapsedTime = 0;

            var allData = fileHandler.Read(request);

            var tripStart = allData.FirstOrDefault(x => x.Contains("TripStart"));
            var tripEnd = allData.FirstOrDefault(x => x.Contains("TripEnd"));

            // remove trip start - end
            Predicate<string> startsWithTrip = line => line.StartsWith("Trip");
            allData.RemoveAll(startsWithTrip);


            if (allData == null || allData?.Count <= 0)
            {
                return new FetchDataResponse();
            }

            string recentRecord = allData.Last();

            var keyValuePairs = recentRecord.Split(',');

            foreach (var pair in keyValuePairs)
            {
                // Split each key-value pair by ":"
                var parts = pair.Split(':');
                if (parts.Length == 2)
                {
                    // Determine the correct type of value (null, int, double, or string)
                    string key = parts[0];
                    object value;

                    if (parts[1] == "null")
                    {
                        value = null;
                    }
                    else if (int.TryParse(parts[1], out int intValue))
                    {
                        value = intValue;
                    }
                    else if (double.TryParse(parts[1], out double doubleValue))
                    {
                        value = doubleValue;
                    }
                    else
                    {
                        value = parts[1];
                    }

                    if (key.StartsWith("10"))
                    {
                        elapsedTime = (DateTime.Now - (long.Parse(parts[1])).ToDateTime()).Milliseconds;
                        deviceTimeStamp = long.Parse(parts[1]);
                    }
                    // Add the key and value to a new list
                    result.Add(new List<object> { key, value, elapsedTime });
                }
            }

            if (!string.IsNullOrEmpty(tripEnd))
            {
                long startTime = long.Parse(tripStart.Split(":")[1]);
                var endTime = long.Parse(tripEnd.Split(":")[1]);

                stat.Elapsed = (endTime.ToDateTime() - startTime.ToDateTime()).Milliseconds;

                stat.Age = new Age
                {
                    Data = (DateTime.Now.ToUniversalTime() - endTime.ToDateTime()).Milliseconds,
                    Ping = 0
                };

                stat.Parked = 0;
            }

            stat.Tick = DateTime.Now.ToTimestamp();
            stat.DevTick = deviceTimeStamp.Value;

            return new FetchDataResponse
            {
                Stats = stat,
                Data = result
            };
        }
    }
}
