using System.Web.Mvc;
using System.Web.UI.WebControls;
using Sitecore.Mvc.Extension.Example.Models;

namespace Sitecore.Mvc.Extension.Example.Controllers
{
  public class SitecoreMvcExtensionController : Controller
  {
    public ActionResult RouteValues(string ExampleRouteValue1)
    {
      ViewBag.ExampleRouteValue1 = ExampleRouteValue1;
      return View("~/Views/Sitecore Mvc Extension Example/ViewWithRoutValue.cshtml");
    }

    public ActionResult ModelTranslation()
    {
      var model = new SampleModel();
      return View("~/Views/Sitecore Mvc Extension Example/ModelTranslation.cshtml", model);
    }
  }
}