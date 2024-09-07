using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Feed;

namespace Team_Plus_Automotive_Telemetry_API.Handlers.Data
{
    public class FeedDataHandler : IHandler<FeedDataRequest, FeedDataResponse>
    {
        private readonly IFileHandler fileHandler;
        public FeedDataHandler(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }
        public FeedDataResponse Handle(FeedDataRequest request)
        {
            var acceptedPairs = fileHandler.Write(request);

            return new FeedDataResponse { Count = acceptedPairs.ToString() };
        }
    }
}
