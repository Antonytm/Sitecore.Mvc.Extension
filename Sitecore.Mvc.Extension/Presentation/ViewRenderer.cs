using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;
using Sitecore.StringExtensions;

namespace Sitecore.Mvc.Extension.Presentation
{
  public class ViewRenderer : Sitecore.Mvc.Presentation.ViewRenderer
  {
    public void Render(TextWriter writer, RouteValueDictionary routeValueDictionary)
    {
      var viewData = new ViewDataDictionary();
      foreach (var value in routeValueDictionary)
      {
        viewData.Add(new KeyValuePair<string, object>(value.Key, value.Value));
      }

      MvcHtmlString str2;
      Assert.IsNotNull(writer, "writer");
      string absoluteViewPath = this.GetAbsoluteViewPath();
      HtmlHelper htmlHelper = this.GetHtmlHelper();
      try
      {
        str2 = htmlHelper.Partial(absoluteViewPath, this.Model, viewData);
      }
      catch (Exception exception)
      {
        string str3 = "Error while rendering view: '{0}'".FormatWith(new object[] { absoluteViewPath });
        if (Model != null)
        {
          Type type = Model.GetType();
          str3 = str3 + " (model: '{0}, {1}')".FormatWith(new object[] { type.FullName, type.Assembly.GetName().Name });
        }
        throw new InvalidOperationException(str3 + ".{0}".FormatWith(new object[] { Environment.NewLine }), exception);
      }
      if (str2 != null)
      {
        writer.Write(str2.ToString());
      }
    }
  }
}