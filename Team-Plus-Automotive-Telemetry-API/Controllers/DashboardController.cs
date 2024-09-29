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

            foreach (var item in data.DataFeed)
            {
                // Split the item by comma
                var splitItems = item.Split(',');

                // Display each split item
                foreach (var splitItem in splitItems)
                {
                    feed.Add(splitItem);
                }
            }

            var model = new DashboardModel
            {
                Parameters = new List<VehicleParameter>()
            };

            foreach (var feedItem in feed)
            {
                var parts = feedItem.Split(':');
                if (parts.Length == 2 && VehicleParameterMapping.Map.ContainsKey(parts[0].Trim()))
                {
                    model.Parameters.Add(new VehicleParameter
                    {
                        Code = parts[0].Trim(),
                        Description = VehicleParameterMapping.Map[parts[0].Trim()],
                        Value = parts[1].Trim()
                    });
                }
            }

            return View("~/Views/Dashboard.cshtml", model);
        }
    }
}
