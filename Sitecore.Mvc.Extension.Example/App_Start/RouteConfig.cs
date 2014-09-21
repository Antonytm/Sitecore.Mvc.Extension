using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sitecore.Mvc.Extension.Example.App_Start.RouteConfig), "RegisterRoutes")]
namespace Sitecore.Mvc.Extension.Example.App_Start
{
  using System.Web.Routing;

  /// <summary>
  /// The route config.
  /// </summary>
  public class RouteConfig
  {
    /// <summary>
    /// The register routes.
    /// </summary>
    public static void RegisterRoutes()
    {
      RouteCollection routes = RouteTable.Routes;
      RouteTable.Routes.MapRoute(
       name: "Sitecore.Mvc.Extension.Example",
       url: "SitecoreMvcExtensionExample/{action}",
       defaults: new
       {
         controller = "SitecoreMvcExtension",
         action = "Index",
       });
    }
  }
}