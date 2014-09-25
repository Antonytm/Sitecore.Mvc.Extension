namespace Sitecore.Mvc.Extension
{
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Helpers;
  using System.Web.Mvc;

  public static class HtmlHelperExtension
  {
    public static SitecoreHelperExtended SitecoreExtended(this HtmlHelper htmlHelper)
    {
      Assert.ArgumentNotNull(htmlHelper, "htmlHelper");
      if(Sitecore.Context.PageMode.IsPageEditor)
        return new SitecoreHelperExtended(htmlHelper);

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
