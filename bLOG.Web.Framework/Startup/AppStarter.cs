using System.Threading;
using bLOG.Core.Localization;

namespace bLOG.Web.Framework.Startup
{
  public class AppStarter
  {
    public static void Start()
    {
      RoutingStartup.Start();
    }
  }
}
