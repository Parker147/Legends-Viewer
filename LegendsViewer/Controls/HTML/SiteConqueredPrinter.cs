using System.Collections.Generic;
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

            HTML.AppendLine("<h1>" + GetTitle() + "</h1></br>");

            HTML.AppendLine(Conquering.GetYearTime() + "The " + Conquering.GetOrdinal(Conquering.Ordinal) + Conquering.ConquerType + " of " + Conquering.Site.ToLink() + " ocurred as a result of " + Conquering.Battle.ToLink() 
                + (Conquering.ParentCollection == null ? "" : " in " + Conquering.ParentCollection.ToLink() + " waged by " + (Conquering.ParentCollection as War).Attacker.PrintEntity() + " on " + (Conquering.ParentCollection as War).Defender.PrintEntity() )
                + ".</br></br>");

            List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, Conquering);

            HTML.AppendLine("<table>");
            HTML.AppendLine("<tr>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("</tr></table></br>");

            HTML.AppendLine("<b>" + Conquering.Attacker.PrintEntity() + " (Attacker)</b></br>");
            HTML.AppendLine("<b>" + Conquering.Defender.PrintEntity() + " (Defender)</b></br></br>");

            PrintEventLog(Conquering.GetSubEvents(), SiteConquered.Filters, Conquering);

            return HTML.ToString();
        }
    }
}
