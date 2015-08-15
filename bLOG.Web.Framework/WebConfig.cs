using System.Collections.Specialized;
using System.Configuration;
using bLOG.Web.Framework.Results.ViewResults;

namespace bLOG.Web.Framework
{
  public static class WebConfig
  {
    public static string Version = "0.11.1";

    public const string ViewsFolder = "Views";
    public const string ViewsExtention = "html";
    public const string LayoutViewName = "Layout";
    public const string NotFoundViewName = "404";

    public const string ViewTokenFormat = "@{0}";
    public const string AnchorFormat = "<a href=\"{0}\">{1}</a>";

    public const string UserUrlFormat = "User.{0}.Url";
    public const string UserEmailFormat = "User.{0}.Email";
    public const string UserGithubFormat = "User.{0}.Github";

    public const string HanlderRoute = "handler";
    public const string ActionRoute = "action";
    public const string IdRoute = "id";


    public static string PageTitleToken = "PageTitle";
    public static string PageBodyToken = "PageBody";
    public static string VersionToken = "Version";

    public static string UrlGithubToken = "UrlGithub";
    public static string UrlLinkedinToken = "UrlLinkedin";
    public static string UrlFacebookToken = "UrlFacebook";
    public static string UrlTwitterToken = "UrlTwitter";
    public static string UrlGoogleToken = "UrlGoogle";

    public static string DisplayGithubToken =   "DisplayGithub";
    public static string DisplayLinkedinToken = "DisplayLinkedin";
    public static string DisplayFacebookToken = "DisplayFacebook";
    public static string DisplayTwitterToken =  "DisplayTwitter";
    public static string DisplayGoogleToken =   "DisplayGoogle";


    public static ViewPathProvider ViewPathProvider = ViewPathProvider.Default;

    public static NameValueCollection AppSettings = ConfigurationManager.AppSettings;
    public static string PageSize = AppSettings["PageSize"];
    public static string BlogTitle = AppSettings["BlogTitle"];

    public static string UrlGithub = AppSettings["Url.Github"];
    public static string UrlLinkedin = AppSettings["Url.Linkedin"];
    public static string UrlFacebook = AppSettings["Url.Facebook"];
    public static string UrlTwitter = AppSettings["Url.Twitter"];
    public static string UrlGoogle = AppSettings["Url.Google"];
  }
}
