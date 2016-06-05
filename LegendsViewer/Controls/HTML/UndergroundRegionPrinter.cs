using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;

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

            int deathCount = Region.Events.OfType<HFDied>().Count();
            if (deathCount > 0 || Region.Battles.Count > 0)
            {
                var popInBattle =
                    Region.Battles
                        .Sum(
                            battle =>
                                battle.AttackerSquads.Sum(squad => squad.Deaths) +
                                battle.DefenderSquads.Sum(squad => squad.Deaths));
                HTML.AppendLine("<b>Deaths</b> " + LineBreak);
                if (deathCount > 100)
                {
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>Historical figures died in this Region: " + deathCount);
                    if (popInBattle > 0)
                    {
                        HTML.AppendLine("<li>Population died in Battle: " + popInBattle);
                    }
                    HTML.AppendLine("</ul>");
                }
                else
                {
                    HTML.AppendLine("<ol>");
                    foreach (HFDied death in Region.Events.OfType<HFDied>())
                    {
                        HTML.AppendLine("<li>" + death.HistoricalFigure.ToLink() + ", in " + death.Year + " (" + death.Cause.GetDescription() + ")");
                    }
                    if (popInBattle > 0)
                    {
                        HTML.AppendLine("<li>Population died in Battle: " + popInBattle);
                    }
                    HTML.AppendLine("</ol>");
                }
            }

            PrintEventLog(Region.Events, UndergroundRegion.Filters, Region);

            return HTML.ToString();
        }
    }
}
