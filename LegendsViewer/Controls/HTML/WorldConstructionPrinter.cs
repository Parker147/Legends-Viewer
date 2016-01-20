using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.HTML
{
    public class WorldConstructionPrinter : HTMLPrinter
    {
        WorldConstruction WorldConstruction;
        World World;

        public WorldConstructionPrinter(WorldConstruction worldConstruction, World world)
        {
            WorldConstruction = worldConstruction;
            World = world;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            HTML.AppendLine("<h1>" + WorldConstruction.Name + ", " + WorldConstruction.Type + "</h1><br />");

            if (WorldConstruction.Coordinates.Any())
            {
                List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, WorldConstruction);

                HTML.AppendLine("<table>");
                HTML.AppendLine("<tr>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("</tr></table></br>");
            }

            HTML.AppendLine("<b>Connects</b><br />");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>" + (WorldConstruction.Site1 != null ? WorldConstruction.Site1.ToLink() : "UNKNOWN SITE") + "</li>");
            HTML.AppendLine("<li>" + (WorldConstruction.Site2 != null ? WorldConstruction.Site2.ToLink() : "UNKNOWN SITE") + "</li>");
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</br>");

            if (WorldConstruction.MasterConstruction != null)
            {
                HTML.AppendLine("<b>Part of</b><br />");
                HTML.AppendLine("<ul>");
                HTML.AppendLine("<li>" + WorldConstruction.MasterConstruction.ToLink() + ", " + WorldConstruction.MasterConstruction.Type + "</li>");
                HTML.AppendLine("</ul>");
                HTML.AppendLine("</br>");
            }

            if (WorldConstruction.Sections.Any())
            {
                HTML.AppendLine("<b>Sections</b><br />");
                HTML.AppendLine("<ul>");
                foreach (WorldConstruction segment in WorldConstruction.Sections)
                {
                    HTML.AppendLine("<li>" + segment.ToLink() + ", " + segment.Type + "</li>");
                }
                HTML.AppendLine("</ul>");
                HTML.AppendLine("</br>");
            }

            PrintEventLog(WorldConstruction.Events, WorldConstruction.Filters, WorldConstruction);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return WorldConstruction.Name;
        }
    }
}
