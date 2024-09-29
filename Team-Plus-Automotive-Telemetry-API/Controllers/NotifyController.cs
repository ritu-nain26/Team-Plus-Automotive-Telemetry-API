using Microsoft.AspNetCore.Mvc;
using Team_Plus_Automotive_Telemetry_API.Handlers;
using Team_Plus_Automotive_Telemetry_API.Models.Common;
using Team_Plus_Automotive_Telemetry_API.Models.Notify;
using Team_Plus_Automotive_Telemetry_API.Utility;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly IHandler<NotifyRequest, string> _loginHandler;
        public NotifyController(ILogger<NotifyController> logger, IHandler<NotifyRequest, string> loginHandler)
        {
            _logger = logger;
            _loginHandler = loginHandler;
        }

        // GET: api/notify/{deviceId}?EV={eventId}&TS={timestamp}&VIN={vin}
        [HttpGet("notify/{deviceId}")]
        public IActionResult Notify(string deviceId, [FromQuery] EventEnum EV, [FromQuery] long TS, [FromQuery] string VIN)
        {
            // Validate and process the parameters
            if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(VIN))
            {
                return BadRequest(new { result = "failed", error = "Missing required parameters" });
            }

            if (!LongExtensions.IsValid(TS))
            {
                return BadRequest(new { result = "failed", error = "Invalid timestamp" });
            }

            if (!WhiteListVehicleIdentificationNumber.Get().Contains(VIN))
            {
                return BadRequest(new { result = "failed", error = "Invalid VIN key" });
            }

            _logger.LogInformation($"Login request from device Id: {deviceId}");

            var request = new NotifyRequest
            {
                VIN = VIN,
                DeviceId = deviceId,
                TimeStamp = TS,
                Event = EV,
            };

            var feedId = _loginHandler.Handle(request);
            // Return success response with feed ID
            return Ok(new { result = "done", id = feedId });
        }
    }
}