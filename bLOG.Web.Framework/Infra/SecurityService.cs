using System.Web.Security;

// ReSharper disable once CheckNamespace
namespace bLOG.Web.Framework
{
  public class SecurityService
  {
    public static bool Authenticate(string username, string password)
    {
      return FormsAuthentication.Authenticate(username, password);
    }
  }
}
