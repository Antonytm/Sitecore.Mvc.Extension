namespace Sitecore.Mvc.Extension
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Mvc;
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Helpers;

  public static class HtmlHelperExtension
  {
    public static SitecoreHelperExtended SitecoreExtended(this HtmlHelper htmlHelper)
    {
      Assert.ArgumentNotNull(htmlHelper, "htmlHelper");
      var threadData = ThreadHelper.GetThreadData<SitecoreHelperExtended>();
      if (threadData == null)
      {
        threadData = new SitecoreHelperExtended(htmlHelper);
        ThreadHelper.SetThreadData<SitecoreHelperExtended>(threadData);
      }
      return threadData;
    }
  }
}
