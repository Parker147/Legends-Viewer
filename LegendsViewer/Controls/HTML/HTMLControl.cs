using System;
using System.Windows.Forms;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    public class HTMLControl : PageControl
    {
        World World;
        public WebBrowser HTMLBrowser;
        private HTMLPrinter Printer;
        public int BrowserScrollPosition;
        public object HTMLObject;

        public HTMLControl(object htmlObject, DwarfTabControl tabControl, World world)
        {
            World = world;
            HTMLObject = htmlObject;
            Printer = HTMLPrinter.GetPrinter(htmlObject, world);
            Title = Printer.GetTitle();
            TabControl = tabControl;
        }

        public override Control GetControl()
        {
            if (HTMLBrowser == null || HTMLBrowser.IsDisposed)
            {
                BrowserUtil.SetBrowserEmulationMode();
                HTMLBrowser = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    WebBrowserShortcutsEnabled = false,
                    DocumentText = Printer.GetHTMLPage(),
                    ScriptErrorsSuppressed = true
                };
                HTMLBrowser.DocumentCompleted += AfterPageLoad;
                HTMLBrowser.Navigating += BrowserNavigating;
                HTMLBrowser.Document.MouseMove += MouseOver;
                return HTMLBrowser;
            }
            return HTMLBrowser;
        }

        public override void Dispose()
        {
            if (HTMLBrowser != null)
            {
                BrowserScrollPosition = HTMLBrowser.Document.Body.ScrollTop;
                HTMLBrowser.Dispose();
                HTMLBrowser = null;
            }
        }

        public override void Refresh()
        {
            HTMLBrowser.Navigate("about:blank");
            HTMLBrowser.DocumentText = Printer.GetHTMLPage();
        }

        private void AfterPageLoad(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).Document.Window.ScrollTo(0, BrowserScrollPosition);
            GC.Collect();
        }

        private void MouseOver(object sender, HtmlElementEventArgs e)
        {
            if (TabControl.Parent.ContainsFocus)
                HTMLBrowser.Document.Focus();
        }

        private void BrowserNavigating(object sender, System.Windows.Forms.WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString() != "about:blank")
            {
                string url = e.Url.ToString();
                url = url.Substring(url.IndexOf(":") + 1, url.Length - url.IndexOf(":") - 1); //remove "about:" at the beginning of the url
                if (!NavigateToNewControl(url))
                    if (!NavigateToNewObjectPage(url))
                        throw new Exception("Could not navigate with url: " + url);
                e.Cancel = true; //Prevent the browser from actually navigating to a new page
            }

        }

        private bool NavigateToNewControl(string url)
        {
            LinkOption option;
            if (Enum.TryParse(url, out option))
            {
                switch (option)
                {
                    case LinkOption.LoadMap:
                        TabControl.Navigate(ControlOption.Map, HTMLObject); break;
                    case LinkOption.LoadChart:
                        TabControl.Navigate(ControlOption.Chart, HTMLObject); break;
                    case LinkOption.LoadSearch:
                        TabControl.Navigate(ControlOption.Search); break;
                }
                return true;
            }
            return false;
        }

        private bool NavigateToNewObjectPage(string url)
        {
            if (url.Contains("#"))
            {
                int id = Convert.ToInt32(url.Substring(url.IndexOf("#") + 1));
                string objectType = url.Substring(0, url.IndexOf("#"));
                object navigateObject = null;
                switch (objectType)
                {
                    case "hf":
                        navigateObject = World.GetHistoricalFigure(id); break;
                    case "region":
                        navigateObject = World.GetRegion(id); break;
                    case "uregion":
                        navigateObject = World.GetUndergroundRegion(id); break;
                    case "site":
                        navigateObject = World.GetSite(id); break;
                    case "entity":
                        navigateObject = World.GetEntity(id); break;
                    case "collection":
                        navigateObject = World.GetEventCollection(id); break;
                    case "era":
                        navigateObject = World.GetEra(id); break;
                    case "artifact":
                        navigateObject = World.GetArtifact(id); break;
                    case "worldconstruction":
                        navigateObject = World.GetWorldConstruction(id); break;
                    default: throw new Exception("Unhandled url type: " + objectType);
                }
                TabControl.Navigate(ControlOption.HTML, navigateObject);
                return true;
            }
            return false;
        }

        //e.Cancel = true;
    }
}
