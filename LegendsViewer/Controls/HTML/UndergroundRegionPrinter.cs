using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    class UndergroundRegionPrinter : HTMLPrinter
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

            HTML.AppendLine("<b>Depth: " + Region.Depth + "</b>");
            HTML.AppendLine("</br></br>");

            if (Region.Battles.Count > 0)
            {
                HTML.AppendLine("<b>Battles</b></br>");
                HTML.AppendLine("<ol>");
                foreach (Battle battle in Region.Battles)
                    HTML.AppendLine(battle.ToLink() + " (" + battle.StartYear + ")");
                HTML.AppendLine("</ol>");
            }

            HTML.AppendLine("<b>Event Log</b> " + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + LineBreak);
            foreach (WorldEvent printEvent in Region.Events)
                if (!UndergroundRegion.Filters.Contains(printEvent.Type))
                    HTML.AppendLine(printEvent.Print(true, Region) + "</br></br>");
            return HTML.ToString();
        }
    }
}
