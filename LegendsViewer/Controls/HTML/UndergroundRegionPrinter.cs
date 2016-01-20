using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends;
using LegendsViewer.Legends.EventCollections;

namespace LegendsViewer.Controls.HTML
{
    public class UndergroundRegionPrinter : HTMLPrinter
    {
        UndergroundRegion Region;
        World World;

        public UndergroundRegionPrinter(UndergroundRegion region, World world)
        {
            Region = region;
            World = world;
        }

        public override string GetTitle()
        {
            return Region.Type;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>Depth: " + Region.Depth + "</h1></br></br>");

            if (Region.Coordinates.Any())
            {
                List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, Region);

                HTML.AppendLine("<table>");
                HTML.AppendLine("<tr>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("</tr></table></br>");
            }

            if (Region.Battles.Count > 0)
            {
                HTML.AppendLine("<b>Battles</b></br>");
                HTML.AppendLine("<ol>");
                foreach (Battle battle in Region.Battles)
                    HTML.AppendLine(battle.ToLink() + " (" + battle.StartYear + ")");
                HTML.AppendLine("</ol>");
            }

            PrintEventLog(Region.Events, UndergroundRegion.Filters, Region);

            return HTML.ToString();
        }
    }
}
