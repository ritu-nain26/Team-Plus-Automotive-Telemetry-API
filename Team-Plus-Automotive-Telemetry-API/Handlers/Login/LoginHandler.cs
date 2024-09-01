using Team_Plus_Automotive_Telemetry_API.Models.Common;
using Team_Plus_Automotive_Telemetry_API.Models.Login;

namespace Team_Plus_Automotive_Telemetry_API.Handlers.Login
{
    public class LoginHandler : IHandler<LoginRequest, LoginResponse>
    {
        public LoginResponse Handle(LoginRequest request)
        {
            var feedId = EncryptionUtility.GenerateEncryptedNumber(request.VIN);

            return new LoginResponse { ID = feedId };
        }
    }
}
