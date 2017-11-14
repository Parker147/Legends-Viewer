using System;
using System.Windows.Forms;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Legends;
using System.Diagnostics;

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

        ~HTMLControl()
        {
            Dispose(false);
        }


        public override Control GetControl()
        {
            if (HTMLBrowser == null || HTMLBrowser.IsDisposed)
            {
                lastNav = DateTime.UtcNow;
                BrowserUtil.SetBrowserEmulationMode();
                HTMLBrowser = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    WebBrowserShortcutsEnabled = false,
                    ScriptErrorsSuppressed = true
                };
                HTMLBrowser.Navigate("about:blank");
                while (HTMLBrowser.Document?.Body == null)
                {
                    Application.DoEvents();
                }
                HTMLBrowser.DocumentText = Printer.GetHTMLPage();
                HTMLBrowser.DocumentCompleted += AfterPageLoad;
                HTMLBrowser.Navigating += BrowserNavigating;
                HTMLBrowser.Document.MouseMove += MouseOver;
                return HTMLBrowser;
            }
            return HTMLBrowser;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DisposePrinter();
                    if (HTMLBrowser?.Document != null)
                    {
                        int newerBrowsers = 0;
                        try
                        {
                            newerBrowsers = HTMLBrowser.Document.GetElementsByTagName("HTML")[0].ScrollTop;
                        }
                        catch (Exception)
                        {
                        }
                        BrowserScrollPosition = newerBrowsers > HTMLBrowser.Document.Body?.ScrollTop
                            ? newerBrowsers
                            : HTMLBrowser.Document.Body.ScrollTop;

                        HTMLBrowser.Dispose();
                        HTMLBrowser = null;
                    }
                }
                base.Dispose(disposing);
                disposed = true;
            }
        }

        public override void Refresh()
        {
            HTMLBrowser.Navigate("about:blank");
            while (HTMLBrowser.Document == null || HTMLBrowser.Document.Body == null)
                Application.DoEvents();
            HTMLBrowser.DocumentText = Printer.GetHTMLPage();

        }

        private DateTime lastNav;
        private void AfterPageLoad(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).Document.Window.ScrollTo(0, BrowserScrollPosition);
            (sender as WebBrowser).Focus();
            GC.Collect();
        }

        private void MouseOver(object sender, HtmlElementEventArgs e)
        {
            if (TabControl.Parent.ContainsFocus)
                HTMLBrowser.Document.Focus();
        }

        private void BrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
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
                        TabControl.Navigate(ControlOption.Map, HTMLObject);
                        break;
                    case LinkOption.LoadChart:
                        TabControl.Navigate(ControlOption.Chart, HTMLObject);
                        break;
                    case LinkOption.LoadSearch:
                        TabControl.Navigate(ControlOption.Search);
                        break;
                    case LinkOption.LoadSiteMap:
                        Site currentSite = HTMLObject as Site;
                        Process.Start(currentSite.SiteMapPath);
                        break;
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
                    case "writtencontent":
                        navigateObject = World.GetWrittenContent(id); break;
                    case "danceform":
                        navigateObject = World.GetDanceForm(id); break;
                    case "musicalform":
                        navigateObject = World.GetMusicalForm(id); break;
                    case "poeticform":
                        navigateObject = World.GetPoeticForm(id); break;
                    case "structure":
                        navigateObject = World.GetStructure(id); break;
                    default: throw new Exception("Unhandled url type: " + objectType);
                }
                TabControl.Navigate(ControlOption.HTML, navigateObject);
                return true;
            }
            return false;
        }

        //e.Cancel = true;
        private void DisposePrinter()
        {

            Printer.DeleteTemporaryFiles();
            Printer.Dispose();
        }
    }
}
