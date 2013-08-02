using System.Linq;
using System.Web.Mvc;

namespace Mvc4Application
{
    public class ExtendedRazorViewEngine : RazorViewEngine
    {
        public ExtendedRazorViewEngine()
            : this(null)
        {
        }

        public ExtendedRazorViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            FileExtensions = new[]
                                 {
                                     "html", "cshtml", "vbhtml"
                                 };

            ViewLocationFormats = new[]
                                      {
                                          "~/public/{0}.html",
                                          "~/public/Shared/{0}.html",
                                      }.Concat(ViewLocationFormats).ToArray();
        }
    }
}