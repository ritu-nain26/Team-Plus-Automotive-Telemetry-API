using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Pull;

namespace Team_Plus_Automotive_Telemetry_API.Handlers.Data
{
    public class PullDataHandler : IHandler<PullDataRequest, PullDataResponse>
    {
        private readonly IFileHandler fileHandler;
        public PullDataHandler(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }
        public PullDataResponse Handle(PullDataRequest request)
        {
            var data = fileHandler.Read(request);

            return new PullDataResponse { DataFeed = data };
        }
    }
}
