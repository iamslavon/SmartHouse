using System.Web.Mvc;

namespace SmartHouse.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Default()
        {
            return View();
        }
    }
}