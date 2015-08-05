using System;
//using bLOG.Core.Localization;
using bLOG.Web.Framework.Startup;

namespace bLOG.Web
{
  public class Global : System.Web.HttpApplication
  {

    protected void Application_Start(object sender, EventArgs e)
    {
      AppStarter.Start();
    }

    protected void Session_Start(object sender, EventArgs e)
    {

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
      //Thread.CurrentThread.CurrentCulture = new PersianCultureInfo();
      //Thread.CurrentThread.CurrentUICulture = new PersianCultureInfo();
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {

    }

    protected void Application_Error(object sender, EventArgs e)
    {

    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {

    }
  }
}