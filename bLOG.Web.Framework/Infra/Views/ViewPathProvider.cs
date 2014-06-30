using System.IO;
using bLOG.Web.Framework;

// ReSharper disable once CheckNamespace
namespace bLOG.Web.Framework.Views
{
  public class ViewPathProvider
  {
    public static ViewPathProvider Default = new ViewPathProvider();

    public string ViewsFolder { get; private set; }
    public string LayoutFileName { get; private set; }
    public string NotFoundFileName { get; private set; }

    public string ViewsExtension { get; private set; }

    public ViewPathProvider(string viewsFolder = WebConfig.ViewsFolder, 
                            string viewsExtention = WebConfig.ViewsExtention,
                            string layoutFileName = WebConfig.LayoutViewName, 
                            string notFoundFileName = WebConfig.NotFoundViewName)
    {
      ViewsFolder = viewsFolder;
      ViewsExtension = viewsExtention;
      LayoutFileName = layoutFileName;
      NotFoundFileName = notFoundFileName;
    }

    private string GetFullName(string name)
    {
      return string.Format("{0}.{1}", name, ViewsExtension);
    }

    public string LayoutViewPath { get { return Path.Combine(ViewsFolder, GetFullName(LayoutFileName)); } }

    public string NotFoundViewPath { get { return Path.Combine(ViewsFolder, GetFullName(NotFoundFileName)); } }

    public string GetPath(string folder, string name)
    {
      return Path.Combine(ViewsFolder, folder, GetFullName(name));
    }
  }
}
