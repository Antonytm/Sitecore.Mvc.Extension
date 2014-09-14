namespace Sitecore.Mvc.Extension
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web;
  using System.Web.Mvc;
  using System.Web.Routing;
  using Sitecore.Mvc.Presentation;
  public class ControllerRunner : Sitecore.Mvc.Controllers.ControllerRunner
  {
    public ControllerRunner(string controllerName, string actionName) : base(controllerName, actionName)
    {
    }

    private RouteValueDictionary routeValues;

    public ControllerRunner(string controllerName, string actionName, RouteValueDictionary routeValues)
      : base(controllerName, actionName)
    {
      this.routeValues = routeValues;
    }

    public virtual string Execute()
    {
      Controller controller = this.GetController();
      TextWriter output = HttpContext.Current.Response.Output;
      StringWriter writer2 = new StringWriter();
      HttpContext.Current.Response.Output = writer2;
      try
      {
        this.ExecuteController(controller);
      }
      finally
      {
        HttpContext.Current.Response.Output = output;
        this.ReleaseController(controller);
      }
      return writer2.ToString();
    }

 



  protected virtual void ExecuteController(Controller controller)
    {
      RequestContext requestContext = PageContext.Current.RequestContext;
      object obj2 = requestContext.RouteData.Values["controller"];
      object obj3 = requestContext.RouteData.Values["action"];
      var routeData = requestContext.RouteData;
      try
      {
        requestContext.RouteData.Values["controller"] = this.ActualControllerName;
        requestContext.RouteData.Values["action"] = this.ActionName;
        foreach (var routeValue in routeValues)
        {
          requestContext.RouteData.Values[routeValue.Key] = routeValue.Value;
        }
        ((IController)controller).Execute(PageContext.Current.RequestContext);
      }
      finally
      {
        requestContext.RouteData.Values["controller"] = obj2;
        requestContext.RouteData.Values["action"] = obj3;
        requestContext.RouteData = routeData;
      }
    }


  }
}
