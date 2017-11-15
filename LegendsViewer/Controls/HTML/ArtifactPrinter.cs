using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.HTML
{
    class ArtifactPrinter : HtmlPrinter
    {
        Artifact _artifact;

        public ArtifactPrinter(Artifact artifact)
        {
            _artifact = artifact;
        }

        public override string Print()
        {
            Html = new StringBuilder();
            Html.AppendLine("<h1>" + _artifact.Name);
            if (!string.IsNullOrWhiteSpace(_artifact.Item) && _artifact.Name != _artifact.Item)
            {
                Html.AppendLine(" \"" + _artifact.Item + "\"");
            }
            Html.AppendLine("</h1>");
            if (!string.IsNullOrWhiteSpace(_artifact.Type))
            {
                Html.AppendLine("<b>" + _artifact.Name + " was a legendary " + _artifact.Material + " ");
                Html.AppendLine((!string.IsNullOrWhiteSpace(_artifact.SubType) ? _artifact.SubType : _artifact.Type.ToLower()) + ".</b><br />");
            }
            if (!string.IsNullOrWhiteSpace(_artifact.Description))
            {
                Html.AppendLine("<i>\"" + _artifact.Description + "\"</i><br />");
            }
            Html.AppendLine("<br />");

            PrintEventLog(_artifact.Events, Artifact.Filters, _artifact);
            return Html.ToString();
        }

        public override string GetTitle()
        {
            return _artifact.Name;
        }
    }
}
