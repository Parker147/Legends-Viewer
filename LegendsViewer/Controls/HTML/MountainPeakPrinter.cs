using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.HTML
{
    public class MountainPeakPrinter : HTMLPrinter
    {
        MountainPeak MountainPeak;
        World World;

        public MountainPeakPrinter(MountainPeak mountainPeak, World world)
        {
            MountainPeak = mountainPeak;
            World = world;
        }

        public override string GetTitle()
        {
            return MountainPeak.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>" + MountainPeak.Name + ", Mountain Peak</h1><br />");

            if (MountainPeak.Coordinates.Any())
            {
                List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, MountainPeak);

                HTML.AppendLine("<table>");
                HTML.AppendLine("<tr>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
                HTML.AppendLine("</tr></table></br>");
            }

            if (MountainPeak.Region != null)
            {
                HTML.AppendLine("<b>Geography</b><br/>");
                HTML.AppendLine("<ul>");
                if (MountainPeak.Region != null)
                {
                    HTML.AppendLine("<li>Region: " + MountainPeak.Region.ToLink() + ", " + MountainPeak.Region.Type.GetDescription() + "</li>");
                }
                HTML.AppendLine("</ul>");
            }

            if (MountainPeak.Height > 0)
            {
                HTML.AppendLine(Bold("Height of " + MountainPeak.ToLink(true, MountainPeak)) + LineBreak);
                HTML.AppendLine("<ul>");
                HTML.AppendLine("<li>" + MountainPeak.Height + " tiles ~ " + (3 * MountainPeak.Height) + " m</li>");
                HTML.AppendLine("</ul>");
            }

            PrintEventLog(MountainPeak.Events, MountainPeak.Filters, MountainPeak);

            return HTML.ToString();
        }
    }
}
