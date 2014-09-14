namespace Sitecore.Mvc.Extension.Pipelines
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Sitecore.Data.Items;
  using Sitecore.Data.Templates;
  using Sitecore.Mvc.Configuration;
  using Sitecore.Mvc.Extensions;
  using Sitecore.Mvc.Names;
  using Sitecore.Mvc.Pipelines.Response.GetRenderer;
  using Sitecore.Mvc.Presentation;
  public class GetControllerRenderer : Sitecore.Mvc.Pipelines.Response.GetRenderer.GetControllerRenderer
  {

    protected override Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
    {
      Tuple<string, string> controllerAndAction = this.GetControllerAndAction(rendering, args);
      if (controllerAndAction == null)
      {
        return null;
      }
      string str = controllerAndAction.Item1;
      string str2 = controllerAndAction.Item2;
      return new ControllerRenderer { ControllerName = str, ActionName = str2 };
    }
  }
}
