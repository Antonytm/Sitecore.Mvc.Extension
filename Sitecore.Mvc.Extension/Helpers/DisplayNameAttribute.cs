namespace Sitecore.Mvc.Extension.Helpers
{
  using Sitecore.Globalization;

  public class DisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
  {
    public DisplayNameAttribute(string displayName)
      : base(displayName)
    {
    }


    public override string DisplayName
    {
      get
      {
        return Translate.Text(base.DisplayName);
      }
    }
  }
}