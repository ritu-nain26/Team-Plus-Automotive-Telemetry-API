using Microsoft.AspNetCore.Mvc;
using Team_Plus_Automotive_Telemetry_API.Models.Login;
using Team_Plus_Automotive_Telemetry_API.Models.Logout;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public LoginResponse Login(LoginRequest request)
        {
            return new LoginResponse { ID = "12345" };
        }

        [HttpPost("logout")]
        public LogoutResponse Logout(LogoutRequest request)
        {
            return new LogoutResponse();
        }
    }
}