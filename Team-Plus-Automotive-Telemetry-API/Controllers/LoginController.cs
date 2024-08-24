using Microsoft.AspNetCore.Mvc;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "login")]
        public string Get()
        {
            return "Login Success";
        }
    }
}