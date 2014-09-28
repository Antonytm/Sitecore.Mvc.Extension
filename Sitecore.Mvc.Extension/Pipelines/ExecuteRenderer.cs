namespace Sitecore.Mvc.Extension.Pipelines
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Sitecore.Mvc.Presentation;
  public class ExecuteRenderer : Sitecore.Mvc.Pipelines.Response.RenderRendering.ExecuteRenderer
  {
    public ExecuteRenderer()
    {
    }

    // Methods
    public void Process(RenderRenderingArgs args)
    {
      if (!args.Rendered)
      {
        Renderer renderer = args.Rendering.Renderer;
        if (renderer != null)
        {
          args.Rendered = this.Render(renderer, args.Writer, args);
        }
      }
    }

    // Methods
    public override void Process(Sitecore.Mvc.Pipelines.Response.RenderRendering.RenderRenderingArgs args)
    {
      if (!args.Rendered)
      {
        Renderer renderer = args.Rendering.Renderer;
        if (renderer != null)
        {
          if (args is Sitecore.Mvc.Extension.Pipelines.RenderRenderingArgs)
          {
            args.Rendered = this.Render(renderer, args.Writer, (RenderRenderingArgs)args);
          }
          else
          {
            args.Rendered = base.Render(renderer, args.Writer, args);
          }
        }
      }
    }

    protected virtual bool Render(Renderer renderer, TextWriter writer, RenderRenderingArgs args)
    {
      if (renderer is Presentation.ControllerRenderer)
      {
        ((Presentation.ControllerRenderer)renderer).Render(writer, args.RouteValues);
      }
      else
      {
        if (renderer is Presentation.ViewRenderer)
        {
          ((Presentation.ViewRenderer)renderer).Render(writer, args.RouteValues);
        }
        else
        {
          renderer.Render(writer);    
        }
      }
      return true;
    }
  }
}
