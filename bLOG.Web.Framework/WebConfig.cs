using System.Collections.Specialized;
using System.Configuration;
using bLOG.Web.Framework.Views;

namespace bLOG.Web.Framework
{
  public static class WebConfig
  {
    public static string Version = "0.6.0";

    public const string ViewsFolder = "Views";
    public const string ViewsExtention = "html";
    public const string LayoutViewName = "Layout";
    public const string NotFoundViewName = "404";

    public const string HanlderRoute = "handler";
    public const string ActionRoute = "action";
    public const string IdRoute = "id";

    public static string ViewTokenFormat = "@{0}";

    public static string PageTitleToken = "PageTitle";
    public static string PageBodyToken = "PageBody";
    public static string VersionToken = "Version";


    public static ViewPathProvider ViewPathProvider = ViewPathProvider.Default;

    public static NameValueCollection AppSettings = ConfigurationManager.AppSettings;
  }
}
