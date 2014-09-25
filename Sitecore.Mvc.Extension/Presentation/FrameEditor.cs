
namespace Sitecore.Mvc.Extension.Presentation
{
  using Sitecore.Web.UI.WebControls;
  using Sitecore.Mvc.Extension;
  using System;
  using System.Web.Mvc;
  using System.Web.UI;
  /// <summary>
  /// The frame editor.
  /// Idea is from http://sitecoreexperiences.blogspot.com/2013/05/sitecore-66-mvc-editframe-helper.html
  /// </summary>
  public class FrameEditor : IDisposable
  {
    private bool disposed;
    private readonly HtmlHelper html;

    public EditFrame EditFrameControl;

    public FrameEditor(HtmlHelper html, string dataSource = null, string buttons = null)
    {
      this.html = html;
      this.EditFrameControl = new EditFrame
      {
        DataSource = dataSource ?? Constants.Paths.HomeItem,
        Buttons = buttons ?? Constants.Paths.FrameEditButtons
      };

      var output = new HtmlTextWriter(html.ViewContext.Writer);
      this.EditFrameControl.RenderFirstPart(output);
    }

    public void Dispose()
    {
      if (disposed) return;
      disposed = true;
      EditFrameControl.RenderLastPart(new HtmlTextWriter(html.ViewContext.Writer));
      EditFrameControl.Dispose();
    }
  }
}