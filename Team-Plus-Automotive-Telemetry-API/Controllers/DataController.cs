using Microsoft.AspNetCore.Mvc;
using Team_Plus_Automotive_Telemetry_API.Handlers;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Pull;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Push;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly IHandler<PushDataRequest, PushDataResponse> _handler;
        private readonly IHandler<PullDataRequest, PullDataResponse> _pullHandler;

        public DataController(ILogger<DataController> logger, IHandler<PushDataRequest, PushDataResponse> handler,
            IHandler<PullDataRequest, PullDataResponse> pullHandler)
        {
            _logger = logger;
            _handler = handler;
            _pullHandler = pullHandler;
        }

        // GET: api/push/{deviceId}
        [HttpGet("{deviceId}")]
        public IActionResult PushData(string deviceId, [FromQuery] Dictionary<string, string> queryParams)
        {
            // Check if timestamp is provided
            if (!queryParams.ContainsKey("TS"))
            {
                return BadRequest("Timestamp (TS) parameter is required.");
            }

            // Try to parse the timestamp
            if (!long.TryParse(queryParams["TS"], out long timestamp))
            {
                return BadRequest("Invalid timestamp format.");
            }

            // Remove timestamp from the queryParams to process remaining telemetry parameters
            queryParams.Remove("TS");

            var request = new PushDataRequest
            {
                DeviceId = deviceId,
                Timestamp = timestamp,
                Parameters = queryParams
            };

            return Ok(_handler.Handle(request));
        }

        [HttpGet("Pull")]
        public IActionResult PullData(string deviceId, long timeStamp)
        {
            var request = new PullDataRequest
            {
                DeviceId = deviceId,
                Timestamp = timeStamp,
            };

            return Ok(_pullHandler.Handle(request));
        }
    }
}
