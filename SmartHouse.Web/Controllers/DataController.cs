using System.Collections.Generic;
using System.Web.Mvc;
using SmartHouse.Services.Data;
using SmartHouse.Web.Models;
using SmartHouse.Web.Models.Response;

namespace SmartHouse.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly DataService dataService;

        public DataController()
        {
            this.dataService = new DataService();
        }

        [HttpPost]
        public ActionResult SetData(IEnumerable<SensorDataViewModel> data)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ErrorResponse("Invalid parameters"));
            }

            foreach (var sensorData in data)
            {
                dataService.SaveSensorData(sensorData.ToSensorData());
            }

            return Json(new SuccessResponse("Saved"));
        }

        [HttpGet]
        public ActionResult GetData()
        {
            return null;
        }
    }
}