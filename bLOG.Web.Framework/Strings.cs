using System.Collections.Specialized;
using System.Configuration;
using bLOG.Web.Framework.Results.ViewResults;

namespace bLOG.Web.Framework
{
    public static class Strings
    {
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


        public const string PageTitleToken = "PageTitle";
        public const string PageBodyToken = "PageBody";
        public const string VersionToken = "Version";
        public const string BlogTitleToken = "BlogTitle";
        public const string CopyrightToken = "Copyright";

        public const string UrlGithubToken = "UrlGithub";
        public const string UrlLinkedinToken = "UrlLinkedin";
        public const string UrlFacebookToken = "UrlFacebook";
        public const string UrlTwitterToken = "UrlTwitter";
        public const string UrlGoogleToken = "UrlGoogle";
        public const string UrlInstagramToken = "UrlInstagram";

        public const string DisplayGithubToken = "DisplayGithub";
        public const string DisplayLinkedinToken = "DisplayLinkedin";
        public const string DisplayFacebookToken = "DisplayFacebook";
        public const string DisplayTwitterToken = "DisplayTwitter";
        public const string DisplayGoogleToken = "DisplayGoogle";
        public const string DisplayInstagramToken = "DisplayInstagram";
    }
}
