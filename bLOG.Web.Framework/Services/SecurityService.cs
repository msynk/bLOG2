using System.Web.Security;

namespace bLOG.Web.Framework.Services
{
    public class SecurityService
    {
        public static bool Authenticate(string username, string password)
        {
            return FormsAuthentication.Authenticate(username, password);
        }
    }
}
