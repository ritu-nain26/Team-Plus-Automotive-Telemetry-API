using Team_Plus_Automotive_Telemetry_API.Models.Data.Pull;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Push;

namespace Team_Plus_Automotive_Telemetry_API.Infrastructure.File
{
    public interface IFileHandler
    {
        public int Write(PushDataRequest pushData);
        public List<string> Read(PullDataRequest request);
    }
}
