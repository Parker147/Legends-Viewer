using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    class ArtifactPrinter : HTMLPrinter
    {
        Artifact Artifact;

        public ArtifactPrinter(Artifact artifact)
        {
            Artifact = artifact;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            //var assembly = Assembly.GetExecutingAssembly();
            //var resourceName = "LegendsViewer.README.md";
            //var markdown = "";
            //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    markdown = reader.ReadToEnd();
            //}
            //var webClient = new WebClient();
            //webClient.Headers.Add("User-Agent", "ghmd-renderer");
            //webClient.Headers.Add("Content-Type", "text/x-markdown");
            //var html =  webClient.UploadString("https://api.github.com/markdown/raw", "POST", markdown);

            //return "<html><head><title>readme</title><link rel='stylesheet' href='https://raw.githubusercontent.com/sindresorhus/github-markdown-css/gh-pages/github-markdown.css'><style>.markdown-body { min-width: 200px; max-width: 790px; margin: 0 auto; padding: 45px; } </style> </head> <body class='markdown-body'>" + html + "</body></html>";
            HTML.AppendLine("<h1>" + Artifact.Name + (!string.IsNullOrWhiteSpace(Artifact.Item) ? " \"" + Artifact.Item + "\"" : "") + "</h1>");
            if (!string.IsNullOrWhiteSpace(Artifact.Type))
            {
                HTML.AppendLine("<b>" + Artifact.Name + " was a legendary " + Artifact.Material + " ");
                HTML.AppendLine((!string.IsNullOrWhiteSpace(Artifact.SubType) ? Artifact.SubType : Artifact.Type) + ".</b><br />");
            }
            if (!string.IsNullOrWhiteSpace(Artifact.Description))
            {
                HTML.AppendLine("<i>\"" + Artifact.Description + "\"</i><br />");
            }
            HTML.AppendLine("<br />");

            PrintEventLog(Artifact.Events, Artifact.Filters, Artifact);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return Artifact.Name;
        }
    }
}
