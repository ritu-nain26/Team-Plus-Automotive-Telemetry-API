﻿using Microsoft.AspNetCore.Mvc;
using Team_Plus_Automotive_Telemetry_API.Handlers;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Feed;
using Team_Plus_Automotive_Telemetry_API.Utility;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly IHandler<FeedDataRequest, FeedDataResponse> _feedDataHandler;
        private readonly IHandler<FetchDataRequest, FetchDataResponse> _fetchDataHandler;

        public DataController(ILogger<DataController> logger,
            IHandler<FeedDataRequest, FeedDataResponse> feedDataHandler,
            IHandler<FetchDataRequest, FetchDataResponse> fetchDataHandler)
        {
            _logger = logger;
            _feedDataHandler = feedDataHandler;
            _fetchDataHandler = fetchDataHandler;
        }

        // GET: api/post/{deviceId}
        [HttpPost("post")]
        public IActionResult FeedData([FromQuery] string deviceId, [FromQuery] long TS, [FromBody] Dictionary<string, string> queryParams)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                return BadRequest(new { result = "failed", error = "deviceId is required" });
            }

            if (!TimestampToDatetime.IsValid(TS))
            {
                return BadRequest(new { result = "failed", error = "Invalid timestamp" });
            }

            var request = new FeedDataRequest
            {
                DeviceId = deviceId,
                Timestamp = TS,
                Parameters = queryParams
            };

            return Ok(_feedDataHandler.Handle(request));
        }

        // GET: api/fetch/{deviceId}/{timeStamp}
        [HttpGet("fetch")]
        public IActionResult FetchData([FromQuery] string deviceId, [FromQuery] long timeStamp)
        {
            var request = new FetchDataRequest
            {
                DeviceId = deviceId,
                Timestamp = timeStamp,
            };

            return Ok(_fetchDataHandler.Handle(request));
        }
    }
}
