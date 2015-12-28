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

            PrintEventLog(Artifact.Events, Artifact.Filters, Artifact);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return Artifact.Name;
        }
    }
}
