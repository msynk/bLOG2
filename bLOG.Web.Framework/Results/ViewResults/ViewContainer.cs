using System.Collections.Generic;
using System.IO;
using System.Web;

namespace bLOG.Web.Framework.Results.ViewResults
{
  public static class ViewContainer
  {
    private static readonly Dictionary<string, string> ContentCache = new Dictionary<string, string>();
    private static readonly Dictionary<string, IViewResult> EngineCache = new Dictionary<string, IViewResult>();

    public static readonly IViewResult LayoutView = GetViewEngine(WebConfig.ViewPathProvider.LayoutViewPath);
    public static readonly IViewResult UnknownRequestView = GetViewEngine(WebConfig.ViewPathProvider.NotFoundViewPath);

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
        EngineCache.Add(virtualPath, new BasicViewResult(virtualPath));
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

    public static IViewResult GetViewEngine(string virtualPath)
    {
      if (!EngineCache.ContainsKey(virtualPath)) Register(virtualPath);
      return EngineCache[virtualPath];
    }
  }
}
