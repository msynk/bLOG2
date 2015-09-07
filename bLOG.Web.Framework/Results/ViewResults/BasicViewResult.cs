using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bLOG.Web.Framework.Results.ViewResults
{
    public class BasicViewResult : IViewResult
    {
        private readonly string _virtualPath;
        private readonly Dictionary<string, object> _replacements = new Dictionary<string, object>();

        public BasicViewResult(string virtualPath)
        {
            _virtualPath = virtualPath;
        }

        public bool UseLayout { get; set; }


        public void ExecuteResult(HttpContext context)
        {
            context.Response.Write(Render());
        }

        public virtual string Render()
        {
            var result = _replacements.Aggregate(ViewContainer.GetContent(_virtualPath),
              (current, replacement) =>
                current.Replace(string.Format(Strings.ViewTokenFormat, replacement.Key), replacement.Value.ToString()));

            return UseLayout ? RenderLayout(result) : result;
        }

        public void ResetTokens()
        {
            _replacements.Clear();
        }

        public void UpdateToken(string token, object value)
        {
            if (_replacements.ContainsKey(token))
            {
                _replacements[token] = value;
            }
            else
            {
                _replacements.Add(token, value);
            }
        }


        private string RenderLayout(string result)
        {
            var layoutView = ViewContainer.LayoutView;
            foreach (var item in _replacements)
            {
                layoutView.UpdateToken(item.Key, item.Value);
            }

            layoutView.UpdateToken(Strings.PageBodyToken, result);
            layoutView.UpdateToken(Strings.VersionToken, WebConfig.Version);
            layoutView.UpdateToken(Strings.BlogTitleToken, WebConfig.Settings.BlogTitle);
            layoutView.UpdateToken(Strings.CopyrightToken, WebConfig.Settings.Copyright);
            SetBlogLinks(layoutView);

            return layoutView.Render();
        }

        private void SetBlogLinks(IViewResult viewResult)
        {
            SetLinkTokens(WebConfig.Settings.UrlGithub,    Strings.UrlGithubToken,    Strings.DisplayGithubToken, viewResult);
            SetLinkTokens(WebConfig.Settings.UrlLinkedin,  Strings.UrlLinkedinToken,  Strings.DisplayLinkedinToken, viewResult);
            SetLinkTokens(WebConfig.Settings.UrlFacebook,  Strings.UrlFacebookToken,  Strings.DisplayFacebookToken, viewResult);
            SetLinkTokens(WebConfig.Settings.UrlTwitter,   Strings.UrlTwitterToken,   Strings.DisplayTwitterToken, viewResult);
            SetLinkTokens(WebConfig.Settings.UrlGoogle,    Strings.UrlGoogleToken,    Strings.DisplayGoogleToken, viewResult);
            SetLinkTokens(WebConfig.Settings.UrlInstagram, Strings.UrlInstagramToken, Strings.DisplayInstagramToken, viewResult);
        }
        private void SetLinkTokens(string url, string urlToken, string displayToken, IViewResult viewResult)
        {
            if (!string.IsNullOrEmpty(url))
            {
                viewResult.UpdateToken(urlToken, url);
                viewResult.UpdateToken(displayToken, "inline");
            }
            else
            {
                viewResult.UpdateToken(displayToken, "none");
            }
        }

    }
}
