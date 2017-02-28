using System.Web.Mvc;

namespace SmartHouse.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Default()
        {
            Response.StatusCode = 500;
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}