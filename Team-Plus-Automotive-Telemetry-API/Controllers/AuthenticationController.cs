using Microsoft.AspNetCore.Mvc;
using Team_Plus_Automotive_Telemetry_API.Handlers.Login;
using Team_Plus_Automotive_Telemetry_API.Models.Common;
using Team_Plus_Automotive_Telemetry_API.Models.Login;
using Team_Plus_Automotive_Telemetry_API.Models.Logout;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly LoginHandler _handler;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
            _handler = new LoginHandler();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            if (request.Event != Models.Common.EventEnum.Login
                || !WhiteListVehicleIdentificationNumber.Get().Contains(request.VIN))
            {
                return BadRequest("Invalid Request.");
            }

            return Ok(_handler.Handle(request));
        }

        [HttpPost("logout")]
        public LogoutResponse Logout([FromBody] LogoutRequest request)
        {
            return new LogoutResponse();
        }
    }
}