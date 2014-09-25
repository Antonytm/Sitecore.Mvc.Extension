
namespace Sitecore.Mvc.Extension
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Links;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Extension.Presentation;
  using Sitecore.Mvc.Pipelines;
  using Sitecore.Mvc.Presentation;
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using System.Web;
  using System.Web.Mvc;
  using System.Web.Mvc.Ajax;
  using System.Web.Routing;

  public class SitecoreHelperExtended : Sitecore.Mvc.Helpers.SitecoreHelper
  {
    public SitecoreHelperExtended(HtmlHelper htmlHelper)
      : base(htmlHelper)
    {
    }

    public virtual HtmlString Placeholder(string placeholderName, object routeValues)
    {
      var rv = TypeHelper.ObjectToDictionary(routeValues);
      return this.Placeholder(placeholderName, rv);
    }

    public virtual HtmlString Placeholder(string placeholderName, RouteValueDictionary routeValues)
    {
      Assert.ArgumentNotNull(placeholderName, "placeholderName");
      using (ContextService.Get().Push<ViewContext>(this.HtmlHelper.ViewContext))
      {
        StringWriter writer = new StringWriter();
        PipelineService.Get()
                       .RunPipeline<Sitecore.Mvc.Extension.Pipelines.RenderPlaceholderExtendedArgs>(PipelineNames.RenderPlaceholder,
                                                                   new Sitecore.Mvc.Extension.Pipelines.RenderPlaceholderExtendedArgs(placeholderName,
                                                                                                     writer,
                                                                                                     RenderingContext
                                                                                                       .Current
                                                                                                       .Rendering,
                                                                                                     routeValues));
        return new HtmlString(writer.ToString());
      }
    }

    public virtual MvcHtmlString ActionLink(string linkText, Item item, ID renderingId, object routeValues,
                                            AjaxOptions ajaxOptions,
                                            object htmlAttributes)
    {
      var rv = TypeHelper.ObjectToDictionary(routeValues);
      var ha = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
      return this.ActionLink(linkText, item, renderingId, rv, ajaxOptions, ha);
    }


    public virtual MvcHtmlString ActionLink(string linkText, Item item, ID renderingId, RouteValueDictionary routeValues,
                                            AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
    {
      var targetUrl = GetTargetUrl(item, renderingId, routeValues);
      TagBuilder builder = new TagBuilder("a")
        {
          InnerHtml = HttpUtility.HtmlEncode(linkText)
        };
      builder.MergeAttributes<string, object>(htmlAttributes);
      builder.MergeAttribute("href", targetUrl);
      builder.MergeAttributes<string, object>(ajaxOptions.ToUnobtrusiveHtmlAttributes());

      return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
    }

    public virtual string UrlLink(Item item, ID renderingId, object routeValues)
    {
      var rv = TypeHelper.ObjectToDictionary(routeValues);
      return GetTargetUrl(item, renderingId, rv);
    }

    private static string GetTargetUrl(Item item, ID renderingId, RouteValueDictionary routeValues)
    {
      var targetUrl = LinkManager.GetItemUrl(item) + "?UseAjax=True&PresentationId=" + renderingId;

      foreach (var routeValue in routeValues)
      {
        if (routeValue.Value != null)
          targetUrl += string.Format("&{0}={1}", routeValue.Key, HttpUtility.UrlEncode(routeValue.Value.ToString()));
      }
      return targetUrl;
    }

    private static string GenerateAjaxScript(AjaxOptions ajaxOptions, string scriptFormat)
    {
      string str = (string)ajaxOptions.GetType().GetMethod("ToJavascriptString").Invoke(ajaxOptions, new object[] { });
      return string.Format(CultureInfo.InvariantCulture, scriptFormat, new object[] { str });
    }

    /// <summary>
    /// The edit frame helper: Renders Sitecore Edit Frame
    /// </summary>
    /// <param name="dataSource">
    /// The data source.
    /// </param>
    /// <param name="buttons">
    /// The buttons.
    /// </param>
    /// <returns>
    /// The <see cref="IDisposable"/>.
    /// </returns>
    public virtual IDisposable EditFrame(string dataSource = null, string buttons = null)
    {
      return new FrameEditor(this.HtmlHelper, dataSource, buttons);
    }
  }
}
