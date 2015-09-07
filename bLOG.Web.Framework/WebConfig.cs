using System.Collections.Specialized;
using System.Configuration;
using bLOG.Web.Framework.Results.ViewResults;

namespace bLOG.Web.Framework
{
    public static class WebConfig
    {
        public static string Version = "0.11.9";

        public static ViewPathProvider ViewPathProvider = ViewPathProvider.Default;
        public static NameValueCollection AppSettings = ConfigurationManager.AppSettings;

        public static class Settings
        {
            public static string PageSize = AppSettings["PageSize"];
            public static string BlogTitle = AppSettings["BlogTitle"];
            public static string Copyright = AppSettings["Copyright"];

            public static string UrlGithub = AppSettings["Url.Github"];
            public static string UrlLinkedin = AppSettings["Url.Linkedin"];
            public static string UrlFacebook = AppSettings["Url.Facebook"];
            public static string UrlTwitter = AppSettings["Url.Twitter"];
            public static string UrlGoogle = AppSettings["Url.Google"];
            public static string UrlInstagram = AppSettings["Url.Instagram"];
        }
    }
}
