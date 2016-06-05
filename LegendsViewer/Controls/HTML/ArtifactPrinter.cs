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
            HTML.AppendLine("<h1>" + Artifact.Name);
            if (!string.IsNullOrWhiteSpace(Artifact.Item) && Artifact.Name != Artifact.Item)
            {
                HTML.AppendLine(" \"" + Artifact.Item + "\"");
            }
            HTML.AppendLine("</h1>");
            if (!string.IsNullOrWhiteSpace(Artifact.Type))
            {
                HTML.AppendLine("<b>" + Artifact.Name + " was a legendary " + Artifact.Material + " ");
                HTML.AppendLine((!string.IsNullOrWhiteSpace(Artifact.SubType) ? Artifact.SubType : Artifact.Type.ToLower()) + ".</b><br />");
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
