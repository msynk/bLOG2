using System.Collections.Generic;
using System.IO;
using System.Web;

// ReSharper disable once CheckNamespace
namespace bLOG.Web.Framework.Views
{
  public static class ViewContainer
  {
    private static readonly Dictionary<string, string> ContentCache = new Dictionary<string, string>();
    private static readonly Dictionary<string, BasicView> EngineCache = new Dictionary<string, BasicView>();

    public static readonly BasicView LayoutView = GetViewEngine(WebConfig.ViewPathProvider.LayoutViewPath);
    public static readonly BasicView UnknownRequestView = GetViewEngine(WebConfig.ViewPathProvider.NotFoundViewPath);

    public static void Reset()
    {
      ContentCache.Clear();
      EngineCache.Clear();
    }

    public static void Register(string virtualPath)
    {
      if (ContentCache.ContainsKey(virtualPath))
      {
        ContentCache.Remove(virtualPath);
        EngineCache.Remove(virtualPath);
      }
      Add(virtualPath);
    }

    private static void Add(string virtualPath)
    {
      using (var reader = new StreamReader(HttpContext.Current.Server.MapPath(GetVirtualPath(virtualPath))))
      {
        ContentCache.Add(virtualPath, reader.ReadToEnd());
        EngineCache.Add(virtualPath, new BasicView(virtualPath));
      }
    }

    private static string GetVirtualPath(string virtualPath)
    {
      const string starter = "~/";
      return virtualPath.StartsWith(starter) ? virtualPath : starter + virtualPath;
    }

    public static string GetContent(string virtualPath)
    {
      if (!ContentCache.ContainsKey(virtualPath)) Register(virtualPath);
      return ContentCache[virtualPath];
    }

    public static BasicView GetViewEngine(string virtualPath)
    {
      if (!EngineCache.ContainsKey(virtualPath)) Register(virtualPath);
      return EngineCache[virtualPath];
    }
  }
}
