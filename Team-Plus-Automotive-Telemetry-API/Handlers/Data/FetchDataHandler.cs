using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch;

namespace Team_Plus_Automotive_Telemetry_API.Handlers.Data
{
    public class FetchDataHandler : IHandler<FetchDataRequest, FetchDataResponse>
    {
        private readonly IFileHandler fileHandler;
        public FetchDataHandler(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }
        public FetchDataResponse Handle(FetchDataRequest request)
        {
            var data = fileHandler.Read(request);
            // Predicate to match items that start with "time"
            Predicate<string> startsWithTime = item => item.StartsWith("Trip");
            // Remove all items that match the predicate
            int removedCount = data.RemoveAll(startsWithTime);

            return new FetchDataResponse { DataFeed = data };
        }
    }
}
