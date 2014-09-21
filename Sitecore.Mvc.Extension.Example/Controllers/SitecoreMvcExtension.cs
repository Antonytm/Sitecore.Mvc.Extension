using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Sitecore.Mvc.Extension.Example.Controllers
{
  public class SitecoreMvcExtensionController : Controller
  {
    public ActionResult RouteValues(string ExampleRouteValue1)
    {
      ViewBag.ExampleRouteValue1 = ExampleRouteValue1;
      return View("~/Views/Sitecore Mvc Extension Example/ViewWithRoutValue.cshtml");
    }
  }
}