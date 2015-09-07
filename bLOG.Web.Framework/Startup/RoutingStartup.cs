using System.Web.Routing;
using bLOG.Web.Framework.Routing;

namespace bLOG.Web.Framework.Startup
{
    class RoutingStartup
    {
        private static readonly RouteCollection Routes = RouteTable.Routes;
        public static void Start()
        {
            RegisterHandlers();
        }

        private static void RegisterHandlers()
        {
            Routes.Ignore("metaweblog");

            //Routes.MapGenericHandlerRoute("Global",
            //  string.Format("{{{0}}}/{{{1}}}/{{{2}}}/{{*pathInfo}}", WebConfig.HanlderRoute, WebConfig.ActionRoute, WebConfig.IdRoute),
            //  string.Format("~/Handlers/{{{0}}}Handler.ashx", WebConfig.HanlderRoute),
            //  new RouteValueDictionary { { WebConfig.HanlderRoute, "Home" }, { WebConfig.ActionRoute, "Index" }, { WebConfig.IdRoute, "1" } });

            Routes.MapHttpHandlerRoute("RSS", "rss", "bLOG.Web.Framework.Handlers.RssHandler");

            Routes.MapHttpHandlerRoute("ShowPost",
              string.Format("post/{{{0}}}/{{*pathInfo}}", Strings.IdRoute),
              string.Format("bLOG.Web.Framework.Handlers.PostHandler"),
              new RouteValueDictionary { { Strings.HanlderRoute, "Post" }, { Strings.ActionRoute, "Index" }, { Strings.IdRoute, "1" } });

            Routes.MapHttpHandlerRoute("Global",
              string.Format("{{{0}}}/{{{1}}}/{{{2}}}/{{*pathInfo}}", Strings.HanlderRoute, Strings.ActionRoute, Strings.IdRoute),
              string.Format("bLOG.Web.Framework.Handlers.{{{0}}}Handler", Strings.HanlderRoute),
              new RouteValueDictionary { { Strings.HanlderRoute, "Home" }, { Strings.ActionRoute, "Index" }, { Strings.IdRoute, "1" } });
        }
    }
}
