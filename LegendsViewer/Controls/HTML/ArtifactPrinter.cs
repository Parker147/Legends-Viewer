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

            if (_artifact.Site != null)
            {
                Html.AppendLine("<b>Current Location:</b><br/>");
                Html.AppendLine("<ul>");
                Html.AppendLine("<li>" + _artifact.Site.ToLink());
                if (_artifact.Structure != null)
                {
                    Html.AppendLine(" (" + _artifact.Structure.ToLink() + ")");
                }
                Html.AppendLine("</li>");
                Html.AppendLine("</ul>");
            }
            if (_artifact.HolderId > 0)
            {
                Html.AppendLine("<b>Current Holder:</b><br/>");
                Html.AppendLine("<ul>");
                Html.AppendLine("<li>" + _artifact.Holder.ToLink() + "</li>");
                Html.AppendLine("</ul>");
            }
            if (_artifact.WrittenContents != null)
            {
                Html.AppendLine("<b>Written Content:</b><br/>");
                Html.AppendLine("<ul>");
                if (_artifact.PageCount > 0)
                {
                    Html.AppendLine("<li>Pages: " + _artifact.PageCount + "</li>");
                }
                foreach (var writtenContent in _artifact.WrittenContents)
                {
                    Html.AppendLine("<li>" + writtenContent.ToLink() + "</li>");
                }
                Html.AppendLine("</ul>");
            }

            PrintEventLog(_artifact.Events, Artifact.Filters, _artifact);
            return Html.ToString();
        }

        public override string GetTitle()
        {
            return _artifact.Name;
        }
    }
}
