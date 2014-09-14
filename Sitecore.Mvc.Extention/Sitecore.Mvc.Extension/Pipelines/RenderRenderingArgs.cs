namespace Sitecore.Mvc.Extension.Pipelines
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Routing;
  using Sitecore.Mvc.Presentation;

  public class RenderRenderingArgs : Sitecore.Mvc.Pipelines.Response.RenderRendering.RenderRenderingArgs
  {
    public RenderRenderingArgs(Rendering rendering, TextWriter writer) : base(rendering, writer)
    {
    }

    public RenderRenderingArgs(Rendering rendering, TextWriter writer, RouteValueDictionary routeValueDictionary)
      : base(rendering, writer)
    {
      this.RouteValues = routeValueDictionary;
    }

    public RouteValueDictionary RouteValues { get; set; }
  }
}
