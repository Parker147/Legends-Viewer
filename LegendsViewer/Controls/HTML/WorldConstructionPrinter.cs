using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.HTML
{
    public class WorldConstructionPrinter : HTMLPrinter
    {
        WorldContruction WorldContruction;
        World World;

        public WorldConstructionPrinter(WorldContruction worldContruction, World world)
        {
            WorldContruction = worldContruction;
            World = world;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            HTML.AppendLine("<h1>" + WorldContruction.Name + "</h1><br />");

            if (WorldContruction.Coordinates.Any())
            {
                List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, WorldContruction);

                HTML.AppendLine("<table>");
                HTML.AppendLine("<tr>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("</tr></table></br>");
            }

            PrintEventLog(WorldContruction.Events, WorldContruction.Filters, WorldContruction);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return WorldContruction.Name;
        }
    }
}
