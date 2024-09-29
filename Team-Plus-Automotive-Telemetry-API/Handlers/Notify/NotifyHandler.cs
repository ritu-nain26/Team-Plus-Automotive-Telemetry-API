using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Common;
using Team_Plus_Automotive_Telemetry_API.Models.Notify;

namespace Team_Plus_Automotive_Telemetry_API.Handlers.Login
{
    public class NotifyHandler : IHandler<NotifyRequest, string>
    {
        private readonly IFileHandler _fileHandler;
        public NotifyHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }
        public string Handle(NotifyRequest request)
        {
            if (request.Event == EventEnum.Login)
            {
                var feedId = EncryptionUtility.GenerateEncryptedNumber(request.VIN);

                // create file here
                _fileHandler.CreateFile(request.DeviceId, request.TimeStamp);

                return feedId;
            }
            else
            {
                _fileHandler.CloseFeed(request.DeviceId, request.TimeStamp);
            }

            return string.Empty;
        }
    }
}
