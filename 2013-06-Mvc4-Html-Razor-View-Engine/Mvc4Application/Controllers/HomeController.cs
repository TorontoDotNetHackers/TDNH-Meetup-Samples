using System.Web.Mvc;

namespace Mvc4Application.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult First(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new {});
        }

        public ActionResult Second(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Second", new {});
        }
    }
}