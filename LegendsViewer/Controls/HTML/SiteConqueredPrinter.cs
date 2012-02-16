using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    class SiteConqueredPrinter : HTMLPrinter
    {
        SiteConquered Conquering;
        World World;

        public SiteConqueredPrinter(SiteConquered conquering, World world)
        {
            Conquering = conquering;
            World = world;
        }

        public override string GetTitle()
        {
            return "The " + Conquering.GetOrdinal(Conquering.Ordinal) + Conquering.ConquerType + " of " + Conquering.Site.ToLink(false);
        }

        public override string Print()
        {
            StringBuilder HTML = new StringBuilder();
            HTML.AppendLine("<basefont size=\"2\">");
            HTML.AppendLine("<style type=\"text/css\">"
                + "a:link {text-decoration: none; color: #000000}"
                + "a:visited {text-decoration: none; color: #000000}"
                + "a:hover {text-decoration: underline; color: #000000}"
                + "td {font-size: 13px;}"
                + "ol { margin-top: 0;}"
                + "ul { margin-top: 0;}"
                + "img {border:none;}"
                + "</style>");

            HTML.AppendLine(Conquering.GetYearTime() + "The " + Conquering.GetOrdinal(Conquering.Ordinal) + Conquering.ConquerType + " of " + Conquering.Site.ToLink() + " ocurred as a result of " + Conquering.Battle.ToLink() + " in " + Conquering.ParentCollection.ToLink() + " waged by " + (Conquering.ParentCollection as War).Attacker.PrintEntity() + " on " + (Conquering.ParentCollection as War).Defender.PrintEntity() + ".</br></br>");

            List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, Conquering);
            HTML.AppendLine("<table border=\"0\" width=\"" + (maps[0].Width + maps[1].Width + 10) + "\">");
            HTML.AppendLine("<tr>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("</tr></table></br>");

            HTML.AppendLine("<b>" + Conquering.Attacker.PrintEntity() + " (Attacker)</b></br>");

            HTML.AppendLine("<b>" + Conquering.Defender.PrintEntity() + " (Defender)</b></br></br>");

            HTML.AppendLine("<b>Event Log</b></br>");
            foreach (WorldEvent printEvent in Conquering.GetSubEvents())
                if (!SiteConquered.Filters.Contains(printEvent.Type))
                    HTML.AppendLine(printEvent.Print(true, Conquering) + "<br/><br/>");
            return HTML.ToString();
        }
    }
}
