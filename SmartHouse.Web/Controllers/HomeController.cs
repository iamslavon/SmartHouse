using System.Web.Mvc;
using SmartHouse.Core.Dto;
using SmartHouse.Services.Data;
using SmartHouse.Web.Models;
using SmartHouse.Web.Models.Response;

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
        public ActionResult SetData(SensorDataViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ErrorResponse("Invalid parameters"));
            }

            var newId = dataService.SaveSensorData(data.ToSensorData());

            return Json(new SuccessResponse("Saved"));
        }
    }
}