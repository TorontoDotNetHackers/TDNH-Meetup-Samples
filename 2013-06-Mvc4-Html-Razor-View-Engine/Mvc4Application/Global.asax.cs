using System.Web;
using System.Web.Mvc;
using System.Web.Razor;
using System.Web.Routing;
using System.Web.WebPages;
using Mvc4Application.App_Start;

namespace Mvc4Application
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Insert(0, new ExtendedRazorViewEngine());
            RazorCodeLanguage.Languages.Add("html", new CSharpRazorCodeLanguage());
            WebPageHttpHandler.RegisterExtension("html");
        }
    }
}