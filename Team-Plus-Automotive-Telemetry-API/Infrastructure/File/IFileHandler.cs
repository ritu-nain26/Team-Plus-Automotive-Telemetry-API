using Team_Plus_Automotive_Telemetry_API.Models.Data.Feed;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch;

namespace Team_Plus_Automotive_Telemetry_API.Infrastructure.File
{
    public interface IFileHandler
    {
        public void CreateFile(string deviceId, long timeStamp);
        public int Write(FeedDataRequest pushData);
        public List<string> Read(FetchDataRequest request);
    }
}
