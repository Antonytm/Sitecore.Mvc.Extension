namespace Sitecore.Mvc.Extension.Pipelines
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Extensions;
  using Sitecore.Mvc.Pipelines;
  using Sitecore.Mvc.Pipelines.Response.RenderPlaceholder;
  using Sitecore.Mvc.Pipelines.Response.RenderRendering;
  using Sitecore.Mvc.Presentation;
  /// <summary>
  /// The perform rendering with routes.
  /// </summary>
  public class PerformRenderingWithRoutes : Sitecore.Mvc.Pipelines.Response.RenderPlaceholder.PerformRendering
  {
    // Methods
    protected virtual Guid GetPageDeviceId(RenderPlaceholderExtendedArgs args)
    {
      Guid guid = args.OwnerRendering.ValueOrDefault<Rendering, Guid>(rendering => rendering.DeviceId);
      if (guid != Guid.Empty)
      {
        return guid;
      }
      Guid guid2 = (PageContext.Current.PageView as RenderingView).ValueOrDefault<RenderingView, Rendering>(view => view.Rendering).ValueOrDefault<Rendering, Guid>(rendering => rendering.DeviceId);
      if (guid2 != Guid.Empty)
      {
        return guid2;
      }
      return Context.Device.ID.ToGuid();
    }

    protected virtual IEnumerable<Rendering> GetRenderings(string placeholderName, RenderPlaceholderExtendedArgs args)
    {
      string placeholderPath = PlaceholderContext.Current.ValueOrDefault<PlaceholderContext, string>(context => context.PlaceholderPath).OrEmpty();
      Guid deviceId = this.GetPageDeviceId(args);
      return args.PageContext.PageDefinition.Renderings.Where<Rendering>(delegate(Rendering r)
      {
        if (!(r.DeviceId == deviceId))
        {
          return false;
        }
        if (!r.Placeholder.EqualsText(placeholderName))
        {
          return r.Placeholder.EqualsText(placeholderPath);
        }
        return true;
      });
    }

    public override void Process(RenderPlaceholderArgs args)
    {
      Assert.ArgumentNotNull(args, "args");
      if (args is RenderPlaceholderExtendedArgs)
        this.Render(args.PlaceholderName, args.Writer, (RenderPlaceholderExtendedArgs)args);
      else
        this.Render(args.PlaceholderName, args.Writer, args);
    }

    public void Process(RenderPlaceholderExtendedArgs args)
    {
      Assert.ArgumentNotNull(args, "args");
      this.Render(args.PlaceholderName, args.Writer, args);
    }


    protected virtual void Render(string placeholderName, TextWriter writer, RenderPlaceholderExtendedArgs args)
    {
      foreach (Rendering rendering in this.GetRenderings(placeholderName, args))
      {
        PipelineService.Get().RunPipeline<RenderRenderingArgs>(PipelineNames.RenderRendering, new RenderRenderingArgs(rendering, writer, args.RouteValues));
      }
    }
  }
}
