using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Push;

namespace Team_Plus_Automotive_Telemetry_API.Handlers.Data
{
    public class PushDataHandler : IHandler<PushDataRequest, PushDataResponse>
    {
        private readonly IFileHandler fileHandler;
        public PushDataHandler(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }
        public PushDataResponse Handle(PushDataRequest request)
        {
            var acceptedPairs = fileHandler.Write(request);

            return new PushDataResponse { Count = acceptedPairs.ToString() };
        }
    }
}
