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
            HTML.AppendLine("<h1>" + WorldConstruction.Name + "</h1><br />");

            if (WorldConstruction.Coordinates.Any())
            {
                List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, WorldConstruction);

                HTML.AppendLine("<table>");
                HTML.AppendLine("<tr>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("</tr></table></br>");
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
