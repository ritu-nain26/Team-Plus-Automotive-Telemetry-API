using Microsoft.AspNetCore.Mvc;
using Team_Plus_Automotive_Telemetry_API.Handlers;
using Team_Plus_Automotive_Telemetry_API.Models.Common;
using Team_Plus_Automotive_Telemetry_API.Models.Dashboard;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch;

namespace Team_Plus_Automotive_Telemetry_API.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHandler<FetchDataRequest, FetchDataResponse> _fetchDataHandler;
        public DashboardController(IHandler<FetchDataRequest, FetchDataResponse> fetchDataHandler)
        {
            _fetchDataHandler = fetchDataHandler;
        }

        // GET: DashboardController
        public ActionResult Index([FromQuery] string deviceId, [FromQuery] long timeStamp)
        {
            var feed = new List<string>();

            var request = new FetchDataRequest
            {
                DeviceId = deviceId,
                Timestamp = timeStamp,
            };

            var data = _fetchDataHandler.Handle(request);

            return View("~/Views/Dashboard.cshtml", data);
        }
    }
}
