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
            return View();
        }

        [HttpGet]
        public ActionResult GetData()
        {
            return Json(new
            {
                Success = true,
                Message = "Some data"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetData(SensorData data)
        {
            var newId = dataService.SaveSensorData(data);

            return Json(new
            {
                Success = true,
                Message = "Saved"
            });
        }
    }
}