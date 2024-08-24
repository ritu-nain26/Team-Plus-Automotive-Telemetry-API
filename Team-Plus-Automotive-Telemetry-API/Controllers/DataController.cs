using Microsoft.AspNetCore.Mvc;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Pull;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Push;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Push")]
        public PushDataResponse PushData(PushDataRequest request)
        {
            return new PushDataResponse();
        }

        [HttpGet("Pull")]
        public PullDataResponse  PullData(PullDataRequest request)
        {
            return new PullDataResponse();
        }
    }
}
