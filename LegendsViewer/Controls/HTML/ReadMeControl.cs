using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using LegendsViewer.Controls.HTML.Utilities;

namespace LegendsViewer.Controls.HTML
{
    public class ReadMeControl : PageControl
    {
        public WebBrowser HTMLBrowser;

        public ReadMeControl(DwarfTabControl tabControl)
        {
            Title = "README.md";
            TabControl = tabControl;
        }

        public override Control GetControl()
        {
            if (HTMLBrowser == null || HTMLBrowser.IsDisposed)
            {
                BrowserUtil.SetBrowserEmulationMode();

                
                var assembly = Assembly.GetExecutingAssembly();
                string readme = "";
                string markdown;
                var resourceName = "LegendsViewer.README.md";
                using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
                {
                    markdown = reader.ReadToEnd();
                }
                try
                {
                    var webClient = new WebClient();
                    webClient.Headers.Add("User-Agent", "ghmd-renderer");
                    webClient.Headers.Add("Content-Type", "text/x-markdown");
                    readme = webClient.UploadString("https://api.github.com/markdown/raw", "POST", markdown);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                var html = "<html>";
                html += "<head>";
                html += "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">";
                html += "<link rel=\"stylesheet\" href=\"" + LocalFileProvider.LocalPrefix +
                        "Controls/HTML/Styles/github-markdown.css\">";
                html += "<style>.markdown-body { margin: 0 auto; padding: 20px; } </style>";
                html += "</head>";
                html += "<body><div class='markdown-body'>" + readme + "</div></body>";
                html += "</html>";

                HTMLBrowser = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    WebBrowserShortcutsEnabled = false,
                    DocumentText = html,
                    ScriptErrorsSuppressed = true
                };
                HTMLBrowser.Navigating += BrowserNavigating;

                return HTMLBrowser;
            }
            return HTMLBrowser;
        }

        private void BrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString() != "about:blank")
            {
                string url = e.Url.ToString();
                Process.Start(url);
                e.Cancel = true; //Prevent the browser from actually navigating to a new page
            }

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (HTMLBrowser != null)
                {
                    HTMLBrowser.Dispose();
                    HTMLBrowser = null;
                }
            }
        }

        public override void Refresh()
        {
        }
    }
}
