using System.Collections.Generic;
using System.Web.Mvc;
using SmartHouse.Core.Dto;
using SmartHouse.Services.Data;

namespace SmartHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataService dataService;

        public HomeController()
        {
            this.dataService = new DataService();
        }

        public ActionResult Index()
        {
            var data = new List<SensorData>
            {
                dataService.GetLastSensorData(1, 1, 1),
                dataService.GetLastSensorData(1, 1, 2)
            };

            return View(data);
        }
    }
}