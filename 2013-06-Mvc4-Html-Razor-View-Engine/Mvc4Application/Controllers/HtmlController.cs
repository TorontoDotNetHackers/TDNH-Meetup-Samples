using System.Web.Mvc;

namespace Mvc4Application.Controllers
{
    public class HtmlController : Controller
    {
        public ActionResult Index(string id)
        {
            return View(id);
        }
    }
}