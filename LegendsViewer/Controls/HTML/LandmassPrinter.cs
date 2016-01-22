using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.HTML
{
    public class LandmassPrinter : HTMLPrinter
    {
        Landmass Landmass;
        World World;

        public LandmassPrinter(Landmass landmass, World world)
        {
            Landmass = landmass;
            World = world;
        }

        public override string GetTitle()
        {
            return Landmass.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>" + Landmass.Name + ", Landmass</h1><br />");

            if (Landmass.Coordinates.Any())
            {
                List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, Landmass);

                HTML.AppendLine("<table>");
                HTML.AppendLine("<tr>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("</tr></table></br>");
            }

            PrintEventLog(Landmass.Events, Landmass.Filters, Landmass);

            return HTML.ToString();
        }
    }
}
