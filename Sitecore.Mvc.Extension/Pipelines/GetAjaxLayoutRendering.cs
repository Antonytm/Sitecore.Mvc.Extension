namespace Sitecore.Mvc.Extension.Pipelines
{
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Pipelines;
  using Sitecore.Mvc.Pipelines.Response.GetPageRendering;
  using Sitecore.Mvc.Pipelines.Response.GetRenderer;
  using Sitecore.Mvc.Presentation;
  using System;
  using System.Linq;
  using System.Web;
  using System.Web.Routing;

  public class GetAjaxLayoutRendering : GetPageRenderingProcessor

  {
    public override void Process(GetPageRenderingArgs args)

    {
      bool useAjax;
      //First of all let's check if we are in ajax mode or not if not don't continue  
      var result = bool.TryParse(HttpContext.Current.Request.Params[Constants.Strings.UseAjaxParameter], out useAjax);
      if (!result || !useAjax)
      {
        return;
      }

      //The second parameter we need to pass is the PresentationId if not present the query if not valid -> don't continue  
      var presentationId = HttpContext.Current.Request.Params[Constants.Strings.PresentationIdParameter];
      if (string.IsNullOrEmpty(presentationId))
      {
        return;
      }

      //If the current item is null return  
      if (Sitecore.Context.Item == null)
      {
        return;
      }

      //Let's resolve the sublayout  
      try
      {
        //Get the list of rendering for the current item  
        var renderings = args.PageDefinition.Renderings;
        //If found  
        if (renderings != null && renderings.Any())
        {
          //Get the first rendering corresponding to the requested one  
          var rendering = renderings.First(sublayout => sublayout.RenderingItem.ID.ToString().Equals(presentationId));
          
          if (rendering != null)
          {
            args.PageDefinition.Renderings.Remove(rendering);
            //Put this rendering into ajax layout  
            rendering.Placeholder = Constants.Strings.AjaxPlaceholderKey;
            
            rendering.LayoutId = new Guid(Constants.IDs.AjaxEmptyLayout);
            var layout = renderings.First(x => x.RenderingType == "Layout");
            if (layout != null)
            {
              args.PageDefinition.Renderings.Remove(layout);
              for ( int i=0; i< args.PageDefinition.Renderings.Count; i++)
              {
                args.PageDefinition.Renderings[i].Placeholder =
                  args.PageDefinition.Renderings[i].Placeholder.Split('/').Last();
                args.PageDefinition.Renderings[i].LayoutId =
                  new Guid(Constants.IDs.AjaxEmptyLayout);
              }
              layout.LayoutId = new Guid(Constants.IDs.AjaxEmptyLayout);
              var getRedererArgs = new GetRendererArgs(new Rendering());
              getRedererArgs.LayoutItem =
                Sitecore.Context.Database.GetItem(Constants.IDs.AjaxEmptyLayout);

              if (rendering.Renderer is Sitecore.Mvc.Presentation.ControllerRenderer)
              {
                RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
                foreach (var key in  HttpContext.Current.Request.QueryString.AllKeys.Select(x=> x.ToString()))
                {
                  routeValueDictionary.Add(key, HttpContext.Current.Request.QueryString[key]);
                }
                ((Presentation.ControllerRenderer)rendering.Renderer).routeValueDictionary = routeValueDictionary;
              }
              layout.Renderer = PipelineService.Get().RunPipeline<GetRendererArgs, Renderer>(PipelineNames.GetRenderer, getRedererArgs, a => a.Result);
              
              args.PageDefinition.Renderings.Add(layout);
              args.PageDefinition.Renderings.Add(rendering);
            }
          }
        }
      }
      catch (Exception exception)
      {
        Log.Warn("Failed to render presentation in ajax layout", exception, this);
      }
    }
  }
}
