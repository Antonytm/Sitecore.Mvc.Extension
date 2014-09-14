using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Sitecore.Mvc.Extensions;

namespace Sitecore.Mvc.Extension.Presentation
{
  public class ControllerRenderer : Sitecore.Mvc.Presentation.ControllerRenderer
  {

    public RouteValueDictionary routeValueDictionary { get; set; }

    public override void Render(TextWriter writer)
    {
      if(routeValueDictionary == null)
        base.Render(writer);
      else
      {
        this.Render(writer, routeValueDictionary);
      }
    }
    // Methods
    public void Render(TextWriter writer, RouteValueDictionary routeValues)
    {
      string controllerName = this.ControllerName;
      string actionName = this.ActionName;
      if (!controllerName.IsWhiteSpaceOrNull() && !actionName.IsWhiteSpaceOrNull())
      {
        string str3 = new ControllerRunner(controllerName, actionName, routeValues).Execute();
        if (!str3.IsEmptyOrNull())
        {
          writer.Write(str3);
        }
      }
    }

  }
}
