using System.Web;

namespace bLOG.Web.Framework.Results.ViewResults
{
    public interface IViewResult : IHttpResult
    {
        bool UseLayout { get; set; }

        string Render();

        void ResetTokens();
        void UpdateToken(string token, object value);
    }
}
