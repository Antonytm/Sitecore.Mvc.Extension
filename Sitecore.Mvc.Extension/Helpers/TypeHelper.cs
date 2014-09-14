namespace Sitecore.Mvc.Extension
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Routing;

  public static class TypeHelper
  {
    public static RouteValueDictionary ObjectToDictionary(object value)
    {
      var dictionary = new RouteValueDictionary();

      Assembly assembly = Assembly.Load(new AssemblyName("System.Web.WebPages"));
      Type typeHelper = assembly.GetType("System.Web.WebPages.TypeHelper");
      MethodInfo mi = typeHelper.GetMethod("ObjectToDictionary", BindingFlags.Public | BindingFlags.Static);
      return (RouteValueDictionary)mi.Invoke(null, new object[] {value});
    }
  }
}
