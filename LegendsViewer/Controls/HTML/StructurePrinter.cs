
using System.Text;
using LegendsViewer.Legends;
using System.Linq;
using LegendsViewer.Legends.Enums;

namespace LegendsViewer.Controls
{
    public class StructurePrinter : HTMLPrinter
    {
        Structure Structure;
        World World;

        public StructurePrinter(Structure structure, World world)
        {
            Structure = structure;
            World = world;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>" + Structure.Name + "</h1>");
            HTML.AppendLine("<b>");
            if (Structure.DungeonType != DungeonType.Unknown)
            {
                HTML.AppendLine(Structure.DungeonType.GetDescription());
            }
            else
            {
                HTML.AppendLine(Structure.Type.GetDescription());
            }
            HTML.AppendLine(" in " + Structure.Site.ToLink() + "</b><br/><br/>");

            if (Structure.Deity != null)
            {
                HTML.AppendLine("<b>Deity:</b><br/>");
                HTML.AppendLine("<ul>");
                HTML.AppendLine("<li>" + Structure.Deity.ToLink() + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (Structure.Religion != null)
            {
                HTML.AppendLine("<b>Religion:</b><br/>");
                HTML.AppendLine("<ul>");
                HTML.AppendLine("<li>" + Structure.Religion.ToLink() + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (Structure.Inhabitants.Any())
            {
                HTML.AppendLine("<b>Inhabitants:</b><br/>");
                HTML.AppendLine("<ul>");
                foreach (var Inhabitant in Structure.Inhabitants)
                {
                    HTML.AppendLine("<li>" + Inhabitant.ToLink() + "</li>");
                }
                HTML.AppendLine("</ul>");
            }

            PrintEventLog(Structure.Events, Structure.Filters, Structure);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return Structure.Name;
        }
    }
}
