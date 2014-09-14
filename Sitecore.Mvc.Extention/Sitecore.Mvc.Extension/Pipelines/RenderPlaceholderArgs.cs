namespace Sitecore.Mvc.Extension.Pipelines
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Routing;
  using Sitecore.Mvc.Pipelines.Response.RenderPlaceholder;
  using Sitecore.Mvc.Presentation;

  /// <summary>
  /// The render placeholder args.
  /// </summary>
  public class RenderPlaceholderExtendedArgs : RenderPlaceholderArgs
  {
    public RenderPlaceholderExtendedArgs(string placeholderName, TextWriter writer) : base(placeholderName, writer)
    {
    }

    public RenderPlaceholderExtendedArgs(string placeholderName, TextWriter writer, Rendering ownerRendering) : base(placeholderName, writer, ownerRendering)
    {
    }
    
    public RenderPlaceholderExtendedArgs(string placeholderName, TextWriter writer, Rendering ownerRendering,  RouteValueDictionary routeValueDictionary)
      : base(placeholderName, writer, ownerRendering)
    {
      this.RouteValues = routeValueDictionary;
    }

    public RouteValueDictionary RouteValues { get; set; }
  }
}
