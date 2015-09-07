using System.IO;

namespace bLOG.Web.Framework.Results.ViewResults
{
    public class ViewPathProvider
    {
        public static ViewPathProvider Default = new ViewPathProvider();

        public string ViewsFolder { get; private set; }
        public string LayoutFileName { get; private set; }
        public string NotFoundFileName { get; private set; }

        public string ViewsExtension { get; private set; }

        public ViewPathProvider(string viewsFolder = Strings.ViewsFolder,
                                string viewsExtention = Strings.ViewsExtention,
                                string layoutFileName = Strings.LayoutViewName,
                                string notFoundFileName = Strings.NotFoundViewName)
        {
            ViewsFolder = viewsFolder;
            ViewsExtension = viewsExtention;
            LayoutFileName = layoutFileName;
            NotFoundFileName = notFoundFileName;
        }

        private string GetFullName(string name)
        {
            return string.Format("{0}.{1}", name, ViewsExtension);
        }

        public string LayoutViewPath { get { return Path.Combine(ViewsFolder, GetFullName(LayoutFileName)); } }

        public string NotFoundViewPath { get { return Path.Combine(ViewsFolder, GetFullName(NotFoundFileName)); } }

        public string GetPath(string folder, string name)
        {
            return Path.Combine(ViewsFolder, folder, GetFullName(name));
        }
    }
}
