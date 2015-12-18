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
            HTML.AppendLine("<h1>" + Artifact.Name + "</h1><br />");
            PrintEvents();
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return Artifact.Name;
        }

        private void PrintEvents()
        {
            HTML.AppendLine("<b>Event Log</b> " + LineBreak);
            foreach (WorldEvent eraEvent in Artifact.Events)
                if (!Artifact.Filters.Contains(eraEvent.Type))
                    HTML.AppendLine(eraEvent.Print(true, Artifact) + "</br></br>");
        }
    }
}
