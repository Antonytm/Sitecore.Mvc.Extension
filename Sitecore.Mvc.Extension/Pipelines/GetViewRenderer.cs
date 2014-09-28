namespace Sitecore.Mvc.Extension.Pipelines
{
  using Sitecore.Mvc.Extensions;
  using Sitecore.Mvc.Pipelines.Response.GetRenderer;
  using Sitecore.Mvc.Presentation;
  using ViewRenderer = Sitecore.Mvc.Extension.Presentation.ViewRenderer;

  public class GetViewRenderer : Sitecore.Mvc.Pipelines.Response.GetRenderer.GetViewRenderer
  {
    public override void Process(GetRendererArgs args)
    {
      if (args.Result == null)
      {
        args.Result = this.GetRenderer(args.Rendering, args);
      }
    }

    protected virtual Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
    {
      string viewPath = this.GetViewPath(rendering, args);
      if (viewPath.IsWhiteSpaceOrNull())
      {
        return null;
      }
      return new ViewRenderer { ViewPath = viewPath, Rendering = rendering };
    }
  }
}